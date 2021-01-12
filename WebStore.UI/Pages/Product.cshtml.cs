using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Application.Products;
using WebStore.Application.Cart;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebStore.UI.Pages
{
    public class ProductModel : PageModel
    {
      
        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public GetProduct.ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGet(
            [FromServices] GetProduct getProduct,
            string name)
        {
            Product = await getProduct.Action(name.Replace("-", " "));
            if (Product == null)
                return RedirectToPage("/Index");
            else
                return Page();
        }

        public async Task<IActionResult> OnPost([FromServices] AddToCart addToCart)
        {
            var stockAdded = await addToCart.Action(CartViewModel);

            if (stockAdded)
                return RedirectToPage("/Cart");
            else
                //TODO Add warning that stock unavailable 
                return Page();
        }
    }
}
