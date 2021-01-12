using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Application.OrdersAdmin;

namespace WebStore.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class OrdersController : Controller
    {
        [HttpGet("")]
        public IActionResult GetOrders(
            int status, 
            [FromServices] GetOrders getOrders) => 
                Ok(getOrders.Action(status));
        
        [HttpGet("{id}")]
        public IActionResult GetOrder(
            int id,
            [FromServices] GetOrder getOrder) => 
                Ok(getOrder.Action(id));

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(
            int id,
            [FromServices] UpdateOrder updateOrder) => 
                Ok(updateOrder.Action(id));
    }
}
