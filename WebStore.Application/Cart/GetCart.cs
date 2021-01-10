using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.Application.Infrastructure;
using WebStore.Database;

namespace WebStore.Application.Cart
{
    public class GetCart
    {
        private readonly ISessionManager _sessionManager;
        private ApplicationDBContext _context;

        public GetCart(ISessionManager sessionManager, ApplicationDBContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }

        public IEnumerable<Response> Action()
        {
            var cartList = _sessionManager.GetCart();
            if (cartList == null)
                return new List<Response>();

            var response = _context.Stock
                .Include(x => x.Product).AsEnumerable()
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Response
                {
                    Name = x.Product.Name,
                    Value = x.Product.Value,
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty,
                    StockId = x.Id,
                    StockDescription = x.Description                     
                })
                .ToList();
            
            return response;
        }

        public class Response
        {
            public string Name { get; set; }
            public decimal Value { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }
            public string StockDescription { get; set; }
        }
    }
}
