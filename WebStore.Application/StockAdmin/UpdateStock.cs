using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Database;
using WebStore.Domain.Models;

namespace WebStore.Application.StockAdmin
{
    public class UpdateStock
    {
        private ApplicationDBContext _context;
        public UpdateStock(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Response> Action(Request request)
        {
            var stocks = new List<Stock>();

            foreach(var stock in request.Stock)
            {
                stocks.Add(new Stock
                {
                    Id = stock.Id,
                    ProductId = stock.ProductId,
                    Description = stock.Description,
                    Qty = stock.Qty
                });
            }

            _context.Stock.UpdateRange(stocks);
            await _context.SaveChangesAsync();

            return new Response
            {
               Stock = request.Stock
            };
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }


        public class Request
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class Response
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
