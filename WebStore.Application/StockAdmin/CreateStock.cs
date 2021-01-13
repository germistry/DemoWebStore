using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Application.StockAdmin
{
    [Service]
    public class CreateStock
    {
        private readonly IStockManager _stockManager;

        public CreateStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public async Task<Response> ActionAsync(Request request)
        {
            var stock = new Stock
            {
                ProductId = request.ProductId,
                StockName = request.StockName,
                Qty = request.Qty
            };

            await _stockManager.CreateStock(stock);

            return new Response 
            { 
                Id = stock.Id,
                StockName = stock.StockName,
                Qty = stock.Qty
            };
        }

        public class Request
        {
            public int ProductId { get; set; }
            public string StockName { get; set; }
            public int Qty { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string StockName { get; set; }
            public int Qty { get; set; }
        }
    }
}
