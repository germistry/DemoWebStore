using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Application.Cart
{
    [Service]
    public class AddToCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly IStockManager _stockManager;

        public AddToCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public async Task<bool> Action(Request request)
        {
            if(!_stockManager.EnoughStock(request.StockId, request.Qty))
            {
                return false;
            }
            await _stockManager.PutStockOnHold(request.StockId, request.Qty, _sessionManager.GetId());

            var stock = _stockManager.GetStockWithProduct(request.StockId);

            var cartProduct = new CartProduct()
            { 
                ProductId = stock.ProductId,
                ProductName = stock.Product.Name,
                ProductImage = stock.Product.Image,
                StockId = stock.Id,
                StockName = stock.StockName,
                Qty = request.Qty,
                MinValue = stock.Product.MinValue

            };

            _sessionManager.AddCartProduct(cartProduct);
            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
