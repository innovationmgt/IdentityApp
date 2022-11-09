using IdentityApp.Authorization;
using IdentityApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Pages.Invoices
{
    public class CreateModel : DI_BasePageModel
    {
        private readonly IdentityApp.Data.IdentityAppContext _context;

        public CreateModel(ApplicationDbContext context,
                           IAuthorizationService authorizationService,
                           UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Invoice.CreateId = UserManager.GetUserId(User);

            var isAuthorized = await Authorizationservice.AuthorizationAysnc(
                UserManager, Invoice, InvoiceOperations.Create);
            if (!isAuthorized.Succeeded == false)
                return Forbid();

            Context.Invoice.Add(Invoice);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
