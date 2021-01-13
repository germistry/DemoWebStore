using System.Collections.Generic;

namespace WebStore.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MinValue { get; set; }
        public ICollection<Stock> Stock { get; set; }
        
    }
}
