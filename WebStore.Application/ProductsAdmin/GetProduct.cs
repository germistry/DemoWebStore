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
                ExtendedDescription = x.ExtendedDescription,
                OGTags = x.OGTags,
                CreatedDate = x.CreatedDate.GetDateTimeAsString(),
                CurrentUpdatedDate = x.UpdatedDate.GetDateTimeAsStringOrNull(),
                UseProductMinValue = x.UseProductMinValue,
                MinValue = x.MinValue,
                CurrentImage = x.Image,
                CategoryId = x.CategoryId
            });
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ExtendedDescription { get; set; }
            public string OGTags { get; set; }
            public string CreatedDate { get; set; } 
            public string CurrentUpdatedDate { get; set; }
            public bool UseProductMinValue { get; set; }
            public decimal MinValue { get; set; }
            public string CurrentImage { get; set; }
            public int CategoryId { get; set; }
        }
    }
}
