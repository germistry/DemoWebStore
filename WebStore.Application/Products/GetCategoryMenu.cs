using System.Collections.Generic;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.Products
{
    [Service]
    public class GetCategoryMenu
    {
        private readonly ICategoryManager _categoryManager;

        public GetCategoryMenu(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public IEnumerable<CategoryViewModel> Action()
        {
            return _categoryManager.GetCategoriesForDropDown(x => new CategoryViewModel
            {
                Id = x.Id,
                CategoryName = x.CategoryName
            });
        }

        public class CategoryViewModel
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }

        }
    }
}
