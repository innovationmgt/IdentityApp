using IdentityApp.Authorization;
using IdentityApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Pages.Invoices
{
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(ApplicationDbContext context, IAuthorizationService authorizationService,
                            UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
        }

        public Invoice Invoice { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await Context.Invoice.FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }
            var isAuthorized = await Authorizationservice.AuthorizeAsync(
                User, Invoice, InvoiceOperations.Read);

            var isManager = User.IsInRole(Constants.InvoiceManagerRole);
            if (isCreator.Succeeded == false && isManager== false)
                return Forbid();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, InvoiceStatus, status)
        {
            Invoice = await Context.Invoice.FindAsync(id);

            if (Invoice == null)
                return NotFound();

            var invoiceOperation = InvoiceStatus == invoiceStatus.Approved
                ? InvoiceOperations.Approve
                : InvoiceOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                UserManager, Invoice, invoiceOperation);

            if (isAuthorized.Succeeded == false)
                return Forbid();

            Invoice.Status = status;
            Context.Invoice.Update(Context.Invoice);

            await Context.SaveChangesAsync();

            return redirectToPage("/Index");
        }
    }
}
