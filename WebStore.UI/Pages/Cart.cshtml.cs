using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStore.Application.Cart;

namespace WebStore.UI.Pages
{
    public class CartModel : PageModel
    {
        public IEnumerable<GetCart.Response> CartList { get; set; }
        public IActionResult OnGet([FromServices] GetCart getCart)
        {
            CartList = getCart.Action();

            return Page();
        }
    }
}
