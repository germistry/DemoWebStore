using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Application.ProductsAdmin;

namespace WebStore.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class ProductsController : Controller
    {
        [HttpGet("getcategories", Name ="getcategories")]
        public IActionResult GetCategoryDropdown(
            [FromServices] GetCategoryDropdown getCategoryDropdown) =>
                Ok(getCategoryDropdown.Action());

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
            CreateProduct.Request request,
            [FromServices] CreateProduct createProduct) =>
                Ok(await createProduct.ActionAsync(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(
            int id, 
            [FromServices] DeleteProduct deleteProduct) =>
                Ok(await deleteProduct.Action(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct(
            UpdateProduct.Request request,
            [FromServices] UpdateProduct updateProduct) =>
                Ok(await updateProduct.ActionAsync(request));
    }
}
