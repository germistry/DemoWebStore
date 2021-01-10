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
        [HttpGet("")]
        public IActionResult GetProducts(
            [FromServices] GetProducts getProducts) => 
                Ok(getProducts.Action());

        [HttpGet("{id}")]
        public IActionResult GetProduct(
            int id, 
            [FromServices] GetProduct getProduct) => 
                Ok(getProduct.Action(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProduct.Request request,
            [FromServices] CreateProduct createProduct) =>
                Ok(await createProduct.ActionAsync(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(
            int id, 
            [FromServices] DeleteProduct deleteProduct) =>
                Ok(await deleteProduct.ActionAsync(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct(
            [FromBody] UpdateProduct.Request request,
            [FromServices] UpdateProduct updateProduct) =>
                Ok(await updateProduct.ActionAsync(request));
    }
}
