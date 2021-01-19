using System.Collections.Generic;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.CategoriesAdmin
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
                Id = x.Id,
                CategoryName = x.CategoryName,
                Description = x.Description
            });
        }

        public class CategoryViewModel
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
 
        }
    }
}
