using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Application.StockAdmin;

namespace WebStore.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class StocksController : Controller
    {
        [HttpGet("")]
        public IActionResult GetStock(
            [FromServices] GetStock getStock) => 
                Ok(getStock.Action());

        [HttpPost("")]
        public async Task<IActionResult> CreateStock(
            [FromBody] CreateStock.Request request, 
            [FromServices] CreateStock createStock) =>
                Ok(await createStock.ActionAsync(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(
            int id,
            [FromServices] DeleteStock deleteStock) =>
                Ok(await deleteStock.Action(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateStock(
            [FromBody] UpdateStock.Request request, 
            [FromServices] UpdateStock updateStock) =>
                Ok(await updateStock.ActionAsync(request));
    }
}
