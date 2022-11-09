using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Authorization
{
    public class InvoiceCreatorAuthorizationHandler :
        AuthorizationHandler<OperationaAuthorizationRequirement, Invoice>
    {
        UserManager<IdentityUser> _userManager;
        public InvoiceCreatorAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = UserManager;
        }
        protected override Task HandlerRequirementAsync(
            AuthorizationHandlerContext context,
            OperationaAuthorizationRequirement requirement, Invoice invoice)
        {
            if (context.User == null || invoice == null)
                return Task.CompletedTask;

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationNmae &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }
            if (invoice.CreatedId == _userManager.GetUserId(context.User))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
