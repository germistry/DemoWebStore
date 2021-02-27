using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Application.CategoriesAdmin
{
    [Service]
    public class CreateCategory
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IFileManager _fileManager;

        public CreateCategory(ICategoryManager categoryManager, IFileManager fileManager)
        {
            _categoryManager = categoryManager;
            _fileManager = fileManager;
        }

        public async Task<Response> ActionAsync(Request request)
        {
            var category = new Domain.Models.Category
            {
                CategoryName = request.CategoryName,
                OGTags = request.OGTags,
                Description = request.Description,
                Image = _fileManager.SaveCategoryImage(request.Image)
            };
            await _categoryManager.CreateCategory(category);

            return new Response
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                OGTags = category.OGTags,
                Description = category.Description,
                CurrentImage = category.Image
            };
        }
        public class Request
        {
            public string CategoryName { get; set; }
            public string OGTags { get; set; }
            public string Description { get; set; }
            public IFormFile Image { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public string OGTags { get; set; }
            public string Description { get; set; }
            public string CurrentImage { get; set; }
        }

    }
}
