using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.Cart
{
    [Service]
    public class DeleteFromCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly IStockManager _stockManager;

        public DeleteFromCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public async Task<bool> Action(Request request)
        {
            if(request.Qty <= 0)
            {
                return false;
            }
            
            await _stockManager.RemoveStockOnHold(request.StockId, request.Qty, _sessionManager.GetId());
            
            _sessionManager.DeleteCartProduct(request.StockId, request.Qty);
            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
           
        }
    }
}
