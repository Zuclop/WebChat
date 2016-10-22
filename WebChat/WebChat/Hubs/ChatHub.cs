using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebChat.Models;

namespace WebChat.Hubs
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// Когда агент логинится в систему - его данные добавляются а список активных агентов.
        /// Когда клиент начинает чат со своей стороны:
        /// 1) для него находится агент
        /// 2) сервер посылает запрос агенту на соединение (получает новый ConnectionId от агента)
        /// 3) формируется навая пара агент-клиент (ClientAgentPairs). 
        /// Чат готов))
        /// </summary>

        public static List<AgentViewModel> AgentsOnline = new List<AgentViewModel>(); 

        public void Send(string to, string message)
        {
            Clients.Client(to).addMessage(message);
        }

        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var callerId = Context.ConnectionId;

            if (Agent)
            {
                AgentsOnline.Find(m => m.Name == userName).ConnectionId = callerId;            
            }
            else
            {                
                if (!AgentsOnline.Any(m => m.Domain == Context.Headers.Get("Origin")))
                {
                    Send(callerId, "We are sorry, all agents are offline...");                     
                    return;
                }

                AgentViewModel agent = AgentsOnline
                    .Where(m => m.Domain == Context.Headers.Get("Origin"))
                    .OrderBy(m => m.clientCounter)
                    .First();

                // выбранному агенту отправляем имя и id нового клиента
                Clients.Client(agent.ConnectionId).onNewClientConnect(userName, callerId);
                agent.clientCounter++;

                // отправляем клиенту имя и id его агента
                Clients.Client(callerId).onAgentConnected(agent.Name, agent.ConnectionId);
            }
        }

        public bool Agent
        {
            get { return Context.Headers.Get("Origin") == "http://localhost:54347"; } // http://WebChat.com in release            
        }
    }
}