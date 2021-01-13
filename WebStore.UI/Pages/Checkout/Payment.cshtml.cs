using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Stripe;
using WebStore.Application.Cart;
using WebStore.Application.Orders;
using WebStore.Domain.Infrastructure;

namespace WebStore.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public string PublicKey { get; }

        public PaymentModel(IConfiguration config)
        {
            PublicKey = config["Stripe:PublicKey"].ToString();
        }
               
        public IActionResult OnGet([FromServices] GetCustomerInfo getCustomerInfo)
        {
            var customerInfo = getCustomerInfo.Action();
            if (customerInfo == null)
            {
                return RedirectToPage("/Checkout/CustomerInfo");
            }
            return Page();
        }
        //TODO Implement Stripe Payment for V3 (currently working only on v1)
        public async Task<IActionResult> OnPost(
            [FromServices] GetCartOrder getCartOrder, 
            [FromServices] CreateOrder createOrder,
            [FromServices] ISessionManager sessionManager,
            string stripeEmail, 
            string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var cartOrder = getCartOrder.Action();

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

            var sessionId = HttpContext.Session.Id;

            await createOrder.Action(new CreateOrder.Request
            {
                StripeRef = charge.Id,
                SessionId = sessionId,
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
            sessionManager.ClearCart();
            return RedirectToPage("/Index");
        }

        
    }
}
