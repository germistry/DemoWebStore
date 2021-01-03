using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStore.Application.Cart;


namespace WebStore.UI.Pages.Checkout
{
    public class CustomerInfoModel : PageModel
    {
        [BindProperty]
        public AddCustomerInfo.Request CustomerInfo { get; set; }

        public IActionResult OnGet()
        {
            var customerInfo = new GetCustomerInfo(HttpContext.Session).Action();
            if(customerInfo == null)
            {
                //Needs to be able to be edited
                return Page();
            }
            else
            {
                return RedirectToPage("/Checkout/Payment");
            } 
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            new AddCustomerInfo(HttpContext.Session).Action(CustomerInfo);

            return RedirectToPage("/Checkout/Payment");
        }
    }
}
