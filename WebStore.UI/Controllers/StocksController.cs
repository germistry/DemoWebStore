using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Application.StockAdmin;
using WebStore.Database;

namespace WebStore.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class StocksController : Controller
    {
        private ApplicationDBContext _context;
        public StocksController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult GetStock() => Ok(new GetStock(_context).Action());

        [HttpPost("")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request request) =>
            Ok(await new CreateStock(_context).Action(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id) =>
            Ok(await new DeleteStock(_context).Action(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request request) =>
            Ok(await new UpdateStock(_context).Action(request));
    }
}
