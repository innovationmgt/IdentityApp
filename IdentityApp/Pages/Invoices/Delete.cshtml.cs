using IdentityApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Pages.Invoices
{
    public class DeleteModel : DI_BasePageModel
    {
        public DeleteModel(ApplicationDbContext context, IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
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
                User, Invoice, InvoiceOperations.Delete);
            if (isAuthorized.Succeeded == false)
                return Forbid();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Invoice = await Context.Invoice.FindAsync(id);

            if (invoice == null)
                return NotFound();

            var isAuthorized = await Authorizationservice.AuthorizeAsync(
                User, Invoice, InvoiceOperations.Delete);
            if (isAuthorized.Succeeded = fasle)
                return Forbid();

            Context.Invoice.Remove(Invoice);
            await Context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
