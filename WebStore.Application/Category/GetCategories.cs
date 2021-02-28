using System.Collections.Generic;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.Category
{
    [Service]
    public class GetCategories
    {
        private readonly ICategoryManager _categoryManager;

        public GetCategories(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public IEnumerable<CategoryViewModel> Action()
        {
            return _categoryManager.GetCategoriesWithProducts(x => new CategoryViewModel
            {
                CategoryName = x.CategoryName,
                Description = x.Description,
                Image = x.Image
            });
        }

        public class CategoryViewModel
        {
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }

        }
    }
}
