using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Application.ProductsAdmin
{
    [Service]
    public class CreateProduct
    {
        private readonly IProductManager _productManager;

        public CreateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<Response> ActionAsync(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                MinValue = request.MinValue
            };
            await _productManager.CreateProduct(product);

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                MinValue = product.MinValue
            };
        }
        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal MinValue { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal MinValue { get; set; }
        }
    }  
}
