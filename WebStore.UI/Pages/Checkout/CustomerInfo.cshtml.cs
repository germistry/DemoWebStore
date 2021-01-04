using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStore.Application.Cart;


namespace WebStore.UI.Pages.Checkout
{
    public class CustomerInfoModel : PageModel
    {
        private IWebHostEnvironment _env;

        public CustomerInfoModel(IWebHostEnvironment env)
        {
            _env = env;
        }


        [BindProperty]
        public AddCustomerInfo.Request CustomerInfo { get; set; }

        public IActionResult OnGet()
        {
            var customerInfo = new GetCustomerInfo(HttpContext.Session).Action();
            if(customerInfo == null)
            {
                if(_env.EnvironmentName == "Development")
                {
                    CustomerInfo = new AddCustomerInfo.Request
                    {
                        FirstName = "Krystal",
                        LastName = "Ruwoldt",
                        EmailAddress = "krystal@germ.com",
                        PhoneNumber = "0414419581",
                        Address1 = "24 Caulfield Road",
                        Address2 = "",
                        City = "Morawa",
                        Postcode = "6623"
                    };
                }

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
