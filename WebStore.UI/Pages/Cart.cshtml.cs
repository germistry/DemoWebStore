using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStore.Application.Cart;
using WebStore.Database;

namespace WebStore.UI.Pages
{
    public class CartModel : PageModel
    {
        private ApplicationDBContext _context;

        public CartModel(ApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<GetCart.Response> CartList { get; set; }
        public IActionResult OnGet()
        {
            CartList = new GetCart(HttpContext.Session, _context).Action();

            return Page();
        }
    }
}
