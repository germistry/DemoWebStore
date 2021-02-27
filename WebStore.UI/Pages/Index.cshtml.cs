using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebStore.Application.Products;

namespace WebStore.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<GetFeaturedProducts.ProductViewModel> Products { get; set; }
        public void OnGet([FromServices] GetFeaturedProducts getFeaturedProducts)
        {
            Products = getFeaturedProducts.Action();
        }
        
    }
}
