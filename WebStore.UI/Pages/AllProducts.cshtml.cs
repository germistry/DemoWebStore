using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Application.Products;
using WebStore.Database;

namespace WebStore.UI.Pages
{
    public class AllProductsModel : PageModel
    {
        private ApplicationDBContext _ctx;

        public AllProductsModel(ApplicationDBContext ctx)
        {
            _ctx = ctx;
        }
       
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }
        public void OnGet()
        {
            Products = new GetProducts(_ctx).Action();
        }
        
    }
}
