using WebStore.Domain.Infrastructure;

namespace WebStore.Application.CategoriesAdmin
{
    [Service]
    public class GetCategory
    {
        private readonly ICategoryManager _categoryManager;

        public GetCategory(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public CategoryViewModel Action(int id)
        {
            return _categoryManager.GetCategoryById(id, x => new CategoryViewModel
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                Description = x.Description,
                OGTags = x.OGTags,
                CurrentImage = x.Image
            });
        }

        public class CategoryViewModel
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public string OGTags { get; set; }
            public string CurrentImage { get; set; }
        }
    }
}
