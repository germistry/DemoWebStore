using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _context.Products.ToList().Select(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = x.Value
            });
        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
