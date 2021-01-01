using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Database;

namespace WebStore.Application.Products
{
    public class GetProduct
    {
        private ApplicationDBContext _context;

        public GetProduct(ApplicationDBContext context)
        {
            _context = context;
        }

        public ProductViewModel Action(string name) =>
            _context.Products
            .Include(x => x.Stock)
            .Where(x => x.Name == name)
            .Select(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = x.Value,

                Stock = x.Stock.Select(y => new StockViewModel 
                { 
                    Id = y.Id,
                    Description = y.Description,
                    InStock = y.Qty > 0
                })
            })
            .FirstOrDefault();
        
        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public bool InStock { get; set; }
        }
    }
}
