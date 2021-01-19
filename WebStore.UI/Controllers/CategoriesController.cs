using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Application.CategoriesAdmin;

namespace WebStore.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class CategoriesController : Controller
    {
        [HttpGet("")]
        public IActionResult GetCategories(
            [FromServices] GetCategories getCategories) =>
                Ok(getCategories.Action());

        [HttpGet("{id}")]
        public IActionResult GetCategory(
            int id,
            [FromServices] GetCategory getCategory) =>
                Ok(getCategory.Action(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateCategory(
            CreateCategory.Request request,
            [FromServices] CreateCategory createCategory) =>
                Ok(await createCategory.ActionAsync(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(
            int id,
            [FromServices] DeleteCategory deleteCategory) =>
                Ok(await deleteCategory.Action(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateCategory(
            UpdateCategory.Request request,
            [FromServices] UpdateCategory updateCategory) =>
                Ok(await updateCategory.ActionAsync(request));
    }
}
