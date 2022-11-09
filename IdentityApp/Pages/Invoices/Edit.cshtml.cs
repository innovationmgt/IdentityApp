using IdentityApp.Data;
using IdentityApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Pages.Invoices
{
    public class EditModel : PageModel : DI_BasePageModel
    {
        public EditModel(
            ApplicationDbContext context, IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base(context, authorizationService, userManager)
    {

    }

    [BindProperty]
    public Invoice Invoice { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || Context.Invoice == null)
        {
            return NotFound();
        }

        var invoice = await Context.Invoice.FirstOrDefaultAsync(m => m.InvoiceId == id);
        if (invoice == null)
        {
            return NotFound();
        }
        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, Invoice, InvoiceOperations.Update);
        if (!isAuthorized.Succeeded == false)
            return Forbid();

        Invoice.Status = invoice.Status;
        IdentityAppContext.Attach(Invoice).State = EntityState.Modified;
        try
        {
            await IdentityAppContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {

        }

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var invoice = await Context.Invoice.AsNoTracking().SingleOrdefaultAsync(m => m.Invoiceid == id);

        if (invoice == null)
            return NotFound();
        Invoice.CreatorId = invoice.CreatorId;

        var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, Invoice, InvoiceOperations.Update);

        if (isAuthorized.Succeeded == false)
            return Forbid();

        Context.Attach(Invoice).State = EntityState.Modified;

        try
        {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InvoiceExists(Invoice.InvoiceId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool InvoiceExists(int id)
    {
        return Context.Invoice.Any(e => e.InvoiceId == id);
    }
}
}
