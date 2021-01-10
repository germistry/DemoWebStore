using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.Database;

namespace WebStore.Application.Products
{
    public class GetProducts
    {
        private ApplicationDBContext _context;

        public GetProducts(ApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Action() =>
            _context.Products
                .Include(x => x.Stock).AsEnumerable()
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Value = x.Value,
                    StockCount = x.Stock.Sum(y => y.Qty)
                })
                .ToList();
        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public int StockCount { get; set; }
            
        }
    }
}
