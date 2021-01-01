using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStore.Database;
using WebStore.Application.Products;
using Microsoft.AspNetCore.Http;

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
        public Test ProductTest { get; set; }
        public class Test
        {
            public string Id { get; set; }
        }

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
            var current_id = HttpContext.Session.GetString("id");

            HttpContext.Session.SetString("id", ProductTest.Id);
            
            return RedirectToPage("Index");
        }
    }
}
