using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Application.ProductsAdmin
{
    [Service]
    public class CreateProduct
    {
        private readonly IProductManager _productManager;
        private readonly IFileManager _fileManager;

        public CreateProduct(IProductManager productManager, IFileManager fileManager)
        {
            _productManager = productManager;
            _fileManager = fileManager;
        }
        public async Task<Response> ActionAsync(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                MinValue = request.MinValue,
                Image = _fileManager.SaveProductImage(request.Image)
            };
            await _productManager.CreateProduct(product);

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                MinValue = product.MinValue,
                CurrentImage = product.Image
            };
        }
        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal MinValue { get; set; }
            public string CurrentImage { get; set; }
            public IFormFile Image { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal MinValue { get; set; }
            public string CurrentImage { get; set; }
        }
    }  
}
