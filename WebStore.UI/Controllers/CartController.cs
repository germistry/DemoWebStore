using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Application.Cart;

namespace WebStore.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        [HttpPost("{stockId}/{qty}")]
        public async Task<IActionResult> AddOne(int stockId, int qty, [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Qty = qty
            };
            
            var success = await addToCart.Action(request);
            if (success)
                return Ok("Item added to cart");
            return BadRequest("Failed to add to cart");      
        }
        [HttpPost("{stockId}/{qty}")]
        public async Task<IActionResult> Remove(int stockId, int qty, [FromServices] DeleteFromCart deleteFromCart)
        {
            var request = new DeleteFromCart.Request
            {
                StockId = stockId,
                Qty = qty
            };

            var success = await deleteFromCart.Action(request);
            if (success)
                return Ok("Item deleted from cart");
            return BadRequest("Failed to delete item from cart");

        }
        [HttpGet]
        public IActionResult GetCartNav([FromServices] GetCart getCart)
        {
            var totalValue = getCart.Action().Sum(x => x.Value * x.Qty);
            return PartialView("Components/Cart/Small", $"${totalValue}");
        }
        [HttpGet]
        public IActionResult GetCartMain([FromServices] GetCart getCart)
        {
            var cart = getCart.Action();
            return PartialView("_CartPartial", cart);
        }
    }
}
