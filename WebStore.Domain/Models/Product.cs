using System;
using System.Collections.Generic;

namespace WebStore.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExtendedDescription { get; set; }
        public string OGTags { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public bool UseProductMinValue { get; set; }
        public decimal MinValue { get; set; }
        public ICollection<Stock> Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
