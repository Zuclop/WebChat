using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebChat.Models;
using WebChat.Hubs;

namespace WebChat.Controllers
{
    public class AgentController : Controller
    {
        // GET: Agent
        public ActionResult Index()
        {
            AgentViewModel agent = new AgentViewModel() { Name = "Vlad", Domain = "http://localhost:12110" };
            ChatHub.AgentsOnline.Add(agent);
            return View(agent);
        }
    }
}