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

        public async Task<IActionResult> OnGet(string name)
        {
            Product = await new GetProduct(_context).Action(name.Replace("-", " "));
            if (Product == null)
                return RedirectToPage("Index");
            else
                return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var stockAdded = await new AddToCart(HttpContext.Session, _context).Action(CartViewModel);

            if (stockAdded)
                return RedirectToPage("Cart");
            else
                //TODO Add warning that stock unavailable 
                return Page();
        }
    }
}
