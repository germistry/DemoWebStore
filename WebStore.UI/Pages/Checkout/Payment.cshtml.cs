using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStore.Application.Cart;

namespace WebStore.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public IActionResult OnGet()
        {
            var customerInfo = new GetCustomerInfo(HttpContext.Session).Action();
            if (customerInfo == null)
            {
                return RedirectToPage("/Checkout/CustomerInfo");
            }
            return Page();
        }
    }
}
