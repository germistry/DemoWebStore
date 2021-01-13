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
                Description = request.Description,
                Qty = request.Qty
            };

            await _stockManager.CreateStock(stock);

            return new Response 
            { 
                Id = stock.Id,
                Description = stock.Description,
                Qty = stock.Qty
            };
        }

        public class Request
        {
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }
}
