using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Stripe;
using WebStore.Application.Cart;
using WebStore.Application.Orders;
using WebStore.Database;

namespace WebStore.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public string PublicKey { get; }

        private ApplicationDBContext _context;

        public PaymentModel(IConfiguration config, ApplicationDBContext context)
        {
            PublicKey = config["Stripe:PublicKey"].ToString();
            _context = context;
        }
               
        public IActionResult OnGet()
        {
            var customerInfo = new GetCustomerInfo(HttpContext.Session).Action();
            if (customerInfo == null)
            {
                return RedirectToPage("/Checkout/CustomerInfo");
            }
            return Page();
        }
        //TODO Implement Stripe Payment for V3 (currently working only on v1)
        public async Task<IActionResult> OnPost(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var cartOrder = new GetOrder(HttpContext.Session, _context).Action();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = cartOrder.GetTotalCharge(),
                Description = "WebStore Charge",
                Currency = "AUD",
                Customer = customer.Id
            });
            
            await new CreateOrder(_context).Action(new CreateOrder.Request
            {
                StripeRef = charge.OrderId,
                FirstName = cartOrder.CustomerInfo.FirstName,
                LastName = cartOrder.CustomerInfo.LastName,
                EmailAddress = cartOrder.CustomerInfo.EmailAddress,
                PhoneNumber = cartOrder.CustomerInfo.PhoneNumber,
                Address1 = cartOrder.CustomerInfo.Address1,
                Address2 = cartOrder.CustomerInfo.Address2,
                City = cartOrder.CustomerInfo.City,
                Postcode = cartOrder.CustomerInfo.Postcode,

                Stocks = cartOrder.Products.Select(x => new CreateOrder.Stock 
                { 
                    StockId = x.StockId,
                    Qty = x.Qty
                }).ToList()
            });

            return RedirectToPage("/Index");
        }

        
    }
}
