using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Application.Cart;

namespace WebStore.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId, [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Qty = 1
            };
            
            var success = await addToCart.Action(request);
            if (success)
                return Ok("Item added to cart");
            return BadRequest("Failed to add to cart");      
        }
        [HttpPost("{stockId}")]
        public async Task<IActionResult> MinusOne(int stockId, [FromServices] DeleteFromCart deleteFromCart)
        {
            var request = new DeleteFromCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var success = await deleteFromCart.Action(request);
            if (success)
                return Ok("Item deleted from cart");
            return BadRequest("Failed to delete item from cart");

        }
        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(int stockId, [FromServices] DeleteFromCart deleteFromCart)
        {
            var request = new DeleteFromCart.Request
            {
                StockId = stockId,
                All = true
            };

            var success = await deleteFromCart.Action(request);
            if (success)
                return Ok("Removed all items from cart");
            return BadRequest("Failed to removed all items from cart");
        }
    }
}
