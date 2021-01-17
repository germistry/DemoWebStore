using System.Collections.Generic;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.Cart
{
    [Service]
    public class GetCart
    {
        private readonly ISessionManager _sessionManager;

        public GetCart(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public IEnumerable<Response> Action()
        {
            return _sessionManager.GetCart(x => new Response
            {
                Name = x.ProductName,
                MinValue = x.MinValue,
                ValueAsString = x.MinValue.GetValueAsString(),
                ProductImage = x.ProductImage,
                    Qty = x.Qty,
                StockId = x.StockId,
                StockName = x.StockName
            }) ;
        }

        public class Response
        {
            public string Name { get; set; }
            public decimal MinValue { get; set; }
            public string ValueAsString { get; set; }
            public string ProductImage { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }
            public string StockName { get; set; }
        }
    }
}
