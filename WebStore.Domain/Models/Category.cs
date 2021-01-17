using System.Collections.Generic;

namespace WebStore.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string OGTags { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<Product> Products { get; set; }
      
    }
}
