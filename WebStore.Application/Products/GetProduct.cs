﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.Products
{
    [Service]
    public class GetProduct
    {
        private readonly IStockManager _stockManager;
        private readonly IProductManager _productManager;

        public GetProduct(IStockManager stockManager, IProductManager productManager)
        {
            _stockManager = stockManager;
            _productManager = productManager;
        }

        public async Task<ProductViewModel> Action(string name)
        {
            await _stockManager.ActionExpiredStockOnHold();

            return _productManager.GetProductByName(name, x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                MinValue = x.MinValue.GetValueAsString(),
                Image = x.Image,

                Stock = x.Stock.Select(y => new StockViewModel
                {
                    Id = y.Id,
                    StockName = y.StockName,
                    Qty = y.Qty,
                    StockValue = y.StockValue.GetValueAsString()
                })
            });
        }
        
        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string MinValue { get; set; }
            public string Image { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string StockName { get; set; }
            public int Qty { get; set; }
            public string StockValue { get; set; }
        }
    }
}
