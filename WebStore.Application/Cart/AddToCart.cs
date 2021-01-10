using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Application.Infrastructure;
using WebStore.Database;
using WebStore.Domain.Models;

namespace WebStore.Application.Cart
{
    public class AddToCart
    {
        private readonly ISessionManager _sessionManager;
        private ApplicationDBContext _context;

        public AddToCart(ISessionManager sessionManager, ApplicationDBContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }

        public interface IStockManager
        {
            bool EnoughStock(int stockId, int qty);
            Task PutStockOnHold(int stockId, int qty, string sessionId);
        }
        public class StockManager : IStockManager
        {
            private ApplicationDBContext _context;

            public StockManager(ApplicationDBContext context)
            {
                _context = context;
            }
            public bool EnoughStock(int stockId, int qty)
            {
                return _context.Stock
                        .FirstOrDefault(x => x.Id == stockId)
                        .Qty >= qty;
            }
            public Task PutStockOnHold(int stockId, int qty, string sessionId)
            {
                _context.Stock.FirstOrDefault(x => x.Id == stockId).Qty -= qty;

                var stockOnHold = _context.StocksOnHold
                    .Where(x => x.SessionId == sessionId)
                    .ToList();
                if (stockOnHold.Any(x => x.StockId == stockId))
                {
                    stockOnHold.Find(x => x.StockId == stockId).Qty += qty;
                }
                else
                {
                    _context.StocksOnHold.Add(new StockOnHold
                    {
                        StockId = stockId,
                        SessionId = sessionId,
                        Qty = qty,
                        ExpiryDate = DateTime.Now.AddMinutes(30)
                    });
                }
                foreach (var stock in stockOnHold)
                { 
                    stock.ExpiryDate = DateTime.Now.AddMinutes(30);
                }
                return _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Action(Request request)
        {

            IStockManager stockManager = new StockManager(_context);

            if(!stockManager.EnoughStock(request.StockId, request.Qty))
            {
                return false;
            }
            await stockManager.PutStockOnHold(request.StockId, request.Qty, _sessionManager.GetId());

            _sessionManager.AddCartProduct(request.StockId, request.Qty);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
