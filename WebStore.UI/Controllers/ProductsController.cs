using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Application.ProductsAdmin;
using WebStore.Database;

namespace WebStore.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class ProductsController : Controller
    {

        private ApplicationDBContext _context;
        public ProductsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Action());

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Action(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Request request) =>
            Ok(await new CreateProduct(_context).Action(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) =>
            Ok(await new DeleteProduct(_context).Action(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Request request) =>
            Ok(await new UpdateProduct(_context).Action(request));
    }
}
