using System.Collections.Generic;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.ProductsAdmin
{
    [Service]
    public class GetProducts
    {
        private readonly IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Action()
        {
            return _productManager.GetProductsWithStock(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedDate = x.CreatedDate.GetDateTimeAsString(),
                LastUpdatedDate = x.UpdatedDate.GetDateTimeAsStringOrNull(),
                UseProductMinValue = x.UseProductMinValue,
                MinValue = x.MinValue,
                CategoryId = x.CategoryId
            });
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string CreatedDate { get; set; }
            public string LastUpdatedDate { get; set; }
            public bool UseProductMinValue { get; set; }
            public decimal MinValue { get; set; }
            public int CategoryId { get; set; }
        }
    }
}
