using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.ProductsAdmin
{
    [Service]
    public class UpdateProduct
    {
        private readonly IProductManager _productManager;
        public UpdateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<Response> ActionAsync(Request request)
        {
            var product = _productManager.GetProductById(request.Id, x => x);
            
            product.Name = request.Name;
            product.Description = request.Description;
            product.MinValue = request.MinValue;

            await _productManager.UpdateProduct(product);

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
            public int Id { get; set; }
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
