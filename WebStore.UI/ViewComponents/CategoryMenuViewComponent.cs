using Microsoft.AspNetCore.Mvc;
using WebStore.Application.Products;

namespace WebStore.UI.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly GetCategoryMenu _getCategoryMenu;
        public CategoryMenuViewComponent(GetCategoryMenu getCategoryMenu)
        {
            _getCategoryMenu = getCategoryMenu;
        }
        public IViewComponentResult Invoke(string view = "Default")
        {
            return View(view, _getCategoryMenu.Action());
        }
    }
}
