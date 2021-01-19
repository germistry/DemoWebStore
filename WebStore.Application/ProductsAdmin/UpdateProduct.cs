using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.ProductsAdmin
{
    [Service]
    public class UpdateProduct
    {
        private readonly IProductManager _productManager;
        private readonly IFileManager _fileManager;

        public UpdateProduct(IProductManager productManager, IFileManager fileManager)
        {
            _productManager = productManager;
            _fileManager = fileManager;
        }
        public async Task<Response> ActionAsync(Request request)
        {
            var product = _productManager.GetProductById(request.Id, x => x);
            
            product.Name = request.Name;
            product.Description = request.Description;
            product.ExtendedDescription = request.ExtendedDescription;
            product.OGTags = request.OGTags;
            product.UpdatedDate = DateTime.Now;
            product.UseProductMinValue = request.UseProductMinValue;
            product.MinValue = request.MinValue;
            if (!string.IsNullOrEmpty(request.CurrentImage))
                _fileManager.RemoveProductImage(request.CurrentImage);
            product.Image = _fileManager.SaveProductImage(request.Image);
            product.CategoryId = request.CategoryId;
                           
            await _productManager.UpdateProduct(product);

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ExtendedDescription = product.ExtendedDescription,
                OGTags = product.OGTags,
                CreatedDate = product.CreatedDate.GetDateTimeAsString(),
                CurrentUpdatedDate = product.UpdatedDate.GetDateTimeAsStringOrNull(),
                UseProductMinValue = product.UseProductMinValue,
                MinValue = product.MinValue,
                CurrentImage = product.Image,
                CategoryId = product.CategoryId
            };
        }
        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ExtendedDescription { get; set; }
            public string OGTags { get; set; }
            public bool UseProductMinValue { get; set; }
            public decimal MinValue { get; set; }
            public string CurrentImage { get; set; }
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
            public string CurrentUpdatedDate { get; set; }
            public bool UseProductMinValue { get; set; }
            public decimal MinValue { get; set; }
            public string CurrentImage { get; set; }
            public int CategoryId { get; set; }
        }
    }
}
