using System;
using System.Collections.Generic;

namespace WebStore.Domain.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string StockName { get; set; }
        public int Qty { get; set; }
        public decimal StockValue { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<OrderStock> OrderStocks { get; set; }
    }
}
