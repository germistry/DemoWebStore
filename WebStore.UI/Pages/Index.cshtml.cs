using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebStore.Application.Category;
using WebStore.Application.Products;

namespace WebStore.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<GetFeaturedProducts.ProductViewModel> Products { get; set; }
        public IEnumerable<GetCategories.CategoryViewModel> Categories { get; set; }
        public void OnGet([FromServices] GetFeaturedProducts getFeaturedProducts, 
            [FromServices] GetCategories getCategories)
        {
            Products = getFeaturedProducts.Action();
            Categories = getCategories.Action();
        }
        
        
    }
}
