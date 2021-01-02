using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStore.Database;
using WebStore.Application.Products;
using Microsoft.AspNetCore.Http;
using WebStore.Application.Cart;

namespace WebStore.UI.Pages
{
    public class ProductModel : PageModel
    {
        private ApplicationDBContext _context;

        public ProductModel(ApplicationDBContext context)
        {
            _context = context;
        }
        
        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public GetProduct.ProductViewModel Product { get; set; }

        public IActionResult OnGet(string name)
        {
            Product = new GetProduct(_context).Action(name.Replace("-", " "));
            if (Product == null)
                return RedirectToPage("Index");
            else
                return Page();
        }

        public IActionResult OnPost()
        {
            new AddToCart(HttpContext.Session).Action(CartViewModel);
            
            return RedirectToPage("Cart");
        }
    }
}
