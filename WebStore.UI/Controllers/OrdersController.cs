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
        private ApplicationDBContext _context;
        public OrdersController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult GetOrders(int status) => Ok(new GetOrders(_context).Action(status));
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id) => Ok(new GetOrder(_context).Action(id));
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id) => Ok((await new UpdateOrder(_context).Action(id)));


    }
}
