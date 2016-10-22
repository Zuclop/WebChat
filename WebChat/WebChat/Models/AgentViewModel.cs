using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChat.Models
{
    public class AgentViewModel
    {
        //public int AgentId { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string ConnectionId { get; set; }
        public int clientCounter { get; set; }

        //public List<string> ConnectionIds { get; set; }
    }
}