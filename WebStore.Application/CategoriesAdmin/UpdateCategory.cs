using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.CategoriesAdmin
{
    [Service]
    public class UpdateCategory
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IFileManager _fileManager;

        public UpdateCategory(ICategoryManager categoryManager, IFileManager fileManager)
        {
            _categoryManager = categoryManager;
            _fileManager = fileManager;
        }

        public async Task<Response> ActionAsync(Request request)
        {
            var category = _categoryManager.GetCategoryById(request.Id, x => x);

            category.CategoryName = request.CategoryName;
            category.Description = request.Description;
            category.OGTags = request.OGTags;
            if (request.Image != null)
            {
                category.Image = _fileManager.SaveCategoryImage(request.Image);
                if (!string.IsNullOrEmpty(request.CurrentImage))
                    _fileManager.RemoveCategoryImage(request.CurrentImage);
            }
            
            await _categoryManager.UpdateCategory(category);

            return new Response
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                OGTags = category.OGTags,
                CurrentImage = category.Image
            };
        }
        public class Request
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public string OGTags { get; set; }
            public string CurrentImage { get; set; }
            public IFormFile Image { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public string OGTags { get; set; }
            public string CurrentImage { get; set; }
        }
    }
}
