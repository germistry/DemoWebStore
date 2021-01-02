using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Database;
using WebStore.Domain.Models;

namespace WebStore.Application.Cart
{
    public class GetCart
    {
        private ISession _session;
        private ApplicationDBContext _context;

        public GetCart(ISession session, ApplicationDBContext context)
        {
            _session = session;
            _context = context;
        }

        public IEnumerable<Response> Action()
        {
            var stringObject = _session.GetString("cart");
            
            if (string.IsNullOrEmpty(stringObject))
                return new List<Response>();
            
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            var response = _context.Stock
                .Include(x => x.Product).AsEnumerable()
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Response
                {
                    Name = x.Product.Name,
                    Value = $"$ {x.Product.Value:N2}",
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty,
                    StockId = x.Id
                })
                .ToList();
            
            return response;
        }

        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }
        }
    }
}
