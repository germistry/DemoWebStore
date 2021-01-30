using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.StockAdmin
{
    [Service]
    public class GetStock
    {
        private readonly IProductManager _productManager;

        public GetStock(IProductManager productManager)
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
                UseProductMinValue = x.UseProductMinValue,
                MinValue = x.MinValue,
                Stock = x.Stock.Select(y => new StockViewModel
                {
                    Id = y.Id,
                    StockName = y.StockName,
                    Qty = y.Qty,
                    StockValue = y.StockValue,
                    CreatedDate = y.CreatedDate.GetDateTimeAsString(),
                    UpdatedDate = y.UpdatedDate.GetDateTimeAsStringOrNull(),
                })
            });
        }
        public class StockViewModel
        { 
            public int Id { get; set; }
            public string StockName { get; set; }
            public int Qty { get; set; }
            public decimal StockValue { get; set; }
            public string CreatedDate { get; set; }
            public string UpdatedDate { get; set; }
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool UseProductMinValue { get; set; }
            public decimal MinValue { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
