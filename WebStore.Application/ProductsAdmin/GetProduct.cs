using WebStore.Domain.Infrastructure;

namespace WebStore.Application.ProductsAdmin
{
    [Service]
    public class GetProduct
    {
        private readonly IProductManager _productManager;

        public GetProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public ProductViewModel Action(int id)
        {
            return _productManager.GetProductById(id, x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                MinValue = x.MinValue
                
            });
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal MinValue { get; set; }
            
        }
    }
}
