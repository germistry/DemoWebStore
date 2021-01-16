using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebStore.Application.Products;

namespace WebStore.UI.Pages
{
    public class AllProductsModel : PageModel
    {
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }
        public void OnGet([FromServices] GetProducts getProducts)
        {
            Products = getProducts.Action();
        }
    }
}
