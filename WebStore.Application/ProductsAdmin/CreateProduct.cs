using Microsoft.AspNetCore.Http;
using System;
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
                ExtendedDescription = request.ExtendedDescription,
                OGTags = request.OGTags,
                CreatedDate = DateTime.Now,
                UseProductMinValue = request.UseProductMinValue,
                MinValue = request.MinValue,
                Image = _fileManager.SaveProductImage(request.Image),
                CategoryId = request.CategoryId
            };
            await _productManager.CreateProduct(product);

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ExtendedDescription = product.ExtendedDescription,
                OGTags = product.OGTags,
                CreatedDate = product.CreatedDate.GetDateTimeAsString(),
                UseProductMinValue = product.UseProductMinValue,
                MinValue = product.MinValue,
                CurrentImage = product.Image
            };
        }
        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string ExtendedDescription { get; set; }
            public string OGTags { get; set; }
            public bool UseProductMinValue { get; set; }
            public decimal MinValue { get; set; }
            public IFormFile Image { get; set; }
            public int CategoryId { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ExtendedDescription { get; set; }
            public string OGTags { get; set; }
            public string CreatedDate { get; set; }
            public bool UseProductMinValue { get; set; }
            public decimal MinValue { get; set; }
            public string CurrentImage { get; set; }
        }
    }  
}
