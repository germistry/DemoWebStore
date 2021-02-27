using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.Products
{
    [Service]
    public class GetFeaturedProducts
    {
        private readonly IProductManager _productManager;

        public GetFeaturedProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Action()
        {
            return _productManager.GetProductsWithStockTop6(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                MinValue = x.MinValue,
                Image = x.Image,
                StockCount = x.Stock.Sum(y => y.Qty)
            });
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal MinValue { get; set; }
            public int StockCount { get; set; }
            public string Image { get; set; }

        }
    }
}
