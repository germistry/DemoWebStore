using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Application.OrdersAdmin;
using WebStore.Database;

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
        public async Task<IActionResult> UpdateOrder(
            int id,
            [FromServices] UpdateOrder updateOrder) => 
                Ok(await updateOrder.ActionAsync(id));
    }
}
