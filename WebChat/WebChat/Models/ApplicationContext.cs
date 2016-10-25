using Microsoft.AspNet.Identity.EntityFramework;
using WebChat.Models;

public class ApplicationContext : IdentityDbContext<AgentViewModel>
{
    public ApplicationContext() : base("IdentityDb") { }

    public static ApplicationContext Create()
    {
        return new ApplicationContext();
    }
}