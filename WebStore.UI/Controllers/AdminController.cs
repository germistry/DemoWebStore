using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Application.ProductsAdmin;
using WebStore.Application.StockAdmin;
using WebStore.Database;

namespace WebStore.UI.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private ApplicationDBContext _context;
        public AdminController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("products")]
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Action());

        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Action(id));

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProduct.Request request) => 
            Ok(await new CreateProduct(_context).Action(request));

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => 
            Ok(await new DeleteProduct(_context).Action(id));

        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody]UpdateProduct.Request request) => 
            Ok(await new UpdateProduct(_context).Action(request));

        [HttpGet("stocks")]
        public IActionResult GetStock() => Ok(new GetStock(_context).Action());

        [HttpPost("stocks")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request request) =>
            Ok(await new CreateStock(_context).Action(request));

        [HttpDelete("stocks/{id}")]
        public async Task<IActionResult> DeleteStock(int id) =>
            Ok(await new DeleteStock(_context).Action(id));

        [HttpPut("stocks")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request request) =>
            Ok(await new UpdateStock(_context).Action(request));
    }
}
