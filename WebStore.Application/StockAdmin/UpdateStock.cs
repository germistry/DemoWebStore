using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Application.StockAdmin
{
    [Service]
    public class UpdateStock
    {
        private readonly IStockManager _stockManager;
        public UpdateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<Response> ActionAsync(Request request)
        {
            var stocks = new List<Stock>();

            foreach(var stock in request.Stock)
            {
                stocks.Add(new Stock
                {
                    Id = stock.Id,
                    ProductId = stock.ProductId,
                    StockName = stock.StockName,
                    Qty = stock.Qty
                });
            }

            await _stockManager.UpdateStockRange(stocks);

            return new Response
            {
               Stock = request.Stock
            };
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string StockName { get; set; }
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
