using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStore.Application.Category;

namespace WebStore.UI.Pages
{
    public class CategoryModel : PageModel
    {
        public GetCategory.CategoryViewModel Category { get; set; }

        public IActionResult OnGet(
            [FromServices] GetCategory getCategory,
            string name)
        {
            Category = getCategory.Action(name.Replace("-", " "));
            if (Category == null)
                return RedirectToPage("/Index");
            else
                return Page();
        }
    }
}
