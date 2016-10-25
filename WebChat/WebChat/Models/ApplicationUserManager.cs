using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using WebChat.Models;

public class ApplicationUserManager : UserManager<AgentViewModel>
{
    public ApplicationUserManager(IUserStore<AgentViewModel> store)
            : base(store)
    {
    }
    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
                                            IOwinContext context)
    {
        ApplicationContext db = context.Get<ApplicationContext>();
        ApplicationUserManager manager = new ApplicationUserManager(new UserStore<AgentViewModel>(db));
        return manager;
    }
}