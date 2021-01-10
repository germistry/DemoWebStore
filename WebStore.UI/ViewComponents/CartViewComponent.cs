using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Application.Cart;

namespace WebStore.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private GetCart _getCart;
        public CartViewComponent(GetCart getCart)
        {
            _getCart = getCart;
        }
        public IViewComponentResult Invoke(string view = "Default")
        {
            if(view == "Small")
            {
                var totalValue = _getCart.Action().Sum(x => x.Value*x.Qty);
                return View(view, $"${totalValue}");
            }
            return View(view, _getCart.Action());
        }
    }
}
