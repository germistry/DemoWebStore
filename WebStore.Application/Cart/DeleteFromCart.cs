using System.Linq;
using System.Threading.Tasks;
using WebStore.Application.Infrastructure;
using WebStore.Database;

namespace WebStore.Application.Cart
{
    public class DeleteFromCart
    {
        private readonly ISessionManager _sessionManager;
        private ApplicationDBContext _context;

        public DeleteFromCart(ISessionManager sessionManager, ApplicationDBContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }

        public async Task<bool> Action(Request request)
        {
            var stockOnHold = _context.StocksOnHold
                .FirstOrDefault(x => 
                    x.StockId == request.StockId && x.SessionId == _sessionManager.GetId());

            var stockInStore = _context.Stock.FirstOrDefault(x => x.Id == request.StockId);
            if(request.All)
            {
                stockInStore.Qty += stockOnHold.Qty;
                _sessionManager.DeleteCartProduct(request.StockId, stockOnHold.Qty);
                stockOnHold.Qty = 0;
            }
            else
            {
                stockInStore.Qty += request.Qty;
                stockOnHold.Qty -= request.Qty;
                _sessionManager.DeleteCartProduct(request.StockId, request.Qty);
            }
            
            if(stockOnHold.Qty <= 0)
            {
                _context.Remove(stockOnHold);
            }
            
            await _context.SaveChangesAsync();

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
            public bool All { get; set; }
        }
    }
}
