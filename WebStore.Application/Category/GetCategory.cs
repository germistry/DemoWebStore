using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.Category
{
    [Service]
    public class GetCategory
    {
        private readonly ICategoryManager _categoryManager;

        public GetCategory(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public CategoryViewModel Action(string name)
        {

            return _categoryManager.GetCategoryByName(name, x => new CategoryViewModel
            {
                CategoryName = x.CategoryName,
                Description = x.Description,
                OGTags = x.OGTags,
                Image = x.Image,

                Product = x.Products.Select(y => new ProductViewModel
                {
                    Id = y.Id,
                    Name = y.Name,
                    Description = y.Description,
                    MinValue = y.MinValue.GetValueAsString(),
                    Image = y.Image
                })
            });
        }

        public class CategoryViewModel
        {
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public string OGTags { get; set; }
            public string Image { get; set; }
            public IEnumerable<ProductViewModel> Product { get; set; }
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string MinValue { get; set; }
            public string Image { get; set; }
        }
    }
}
