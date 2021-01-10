using System.Linq;
using WebStore.Database;

namespace WebStore.Application.ProductsAdmin
{
    public class GetProduct
    {
        private ApplicationDBContext _context;

        public GetProduct(ApplicationDBContext context)
        {
            _context = context;
        }

        public ProductViewModel Action(int id) =>
            _context.Products.Where(x => x.Id == id).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = x.Value
            })
            .FirstOrDefault();
        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
