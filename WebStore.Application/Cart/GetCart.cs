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
                    Value = x.Value,
                    ValueAsString = x.Value.GetValueAsString(),
                    Qty = x.Qty,
                    StockId = x.StockId,
                    StockDescription = x.StockDescription
                });
        }

        public class Response
        {
            public string Name { get; set; }
            public decimal Value { get; set; }
            public string ValueAsString { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }
            public string StockDescription { get; set; }
        }
    }
}
