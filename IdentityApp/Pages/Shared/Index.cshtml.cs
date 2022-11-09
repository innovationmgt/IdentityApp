using IdentityApp.Models;

namespace IdentityApp.Pages.Shared
{
    public class IndexModel : PageModel
    {
        public Dictionary<string, int> revenueSubmitted;
        public Dictionary<string, int> revenueApproved;
        public Dictionary<string, int> revenueRejected;

        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext -context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;

            _context = context;
        }

        public void OnGet()
        {
            InitDict(ref revenueSubmitted);
            InitDict(ref revenueApproved);
            InitDict(ref revenueRejected);

            var invoices = _context.Invoice.ToList();

            foreach (var invoice in invoices)
            {
                switch (invoice.Status)
                {
                    case InvoiceStatus.Submitted:
                        revenueSubmitted[invoce.InvoiceMonth] += (int)invoice.InvoiceAmount;
                        break;
                    case InvoiceStatus.Approved:
                        revenueApproved[invoce.InvoiceMonth] += (int)invoice.InvoiceAmount;
                        break;
                    case InvoiceStatus.Rejected:
                        revenueRejected[invoce.InvoiceMonth] += (int)invoice.InvoiceAmount;
                        break;
                    default:
                        break;
                
                }
            }
        }

        private void InitDict(ref Dictionary<string, int>dict)
        {
            dict = new Dictionary<string, int>
            {
                {"Jan", 0},
                {"Feb", 0},
                {"Mar",0},
                {"Apr",0},
                {"May",1},
                {"Jun",0},
                {"Jul",0},
                {"Aug",0},
                {"Sep",0 },
                {"Oct",0 },
                {"Nov",0},
                {"Dec",0 }
            };
        }
    }
}
