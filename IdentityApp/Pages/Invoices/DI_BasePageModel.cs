using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Pages.Invoices
{
    public class DI_BasePageModel : PageModel
    {
        protected ApplicationDbContext Context { get; }
        protected IAuthorizationService Authorizationservice { get; }
        protected UserManager<IdentityUser> UserManager { get; }

        public DI_BasePageModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
        {
            Context = context;
            AuthorizationService = authorizationService;
            UserManager = userManager;
        }
    }
}
