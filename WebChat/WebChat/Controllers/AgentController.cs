using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebChat.Models;
using WebChat.Hubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace WebChat.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        // GET: Agent
        public ActionResult Index()
        {
            AgentViewModel agent = UserManager.FindById(User.Identity.GetUserId());
            if (agent.Name == null)
                agent.Name = agent.Email;
            ChatHub.AgentsOnline.Add(agent);
            return View(agent);
        }
    }
}