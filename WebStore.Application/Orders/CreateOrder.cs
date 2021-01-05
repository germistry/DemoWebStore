using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Database;
using WebStore.Domain.Models;

namespace WebStore.Application.Orders
{
    public class CreateOrder
    {
        private ApplicationDBContext _context;
        public CreateOrder(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Action(Request request)
        {
            var stocksOnHold = _context.StocksOnHold
                .Where(x => x.SessionId == request.SessionId)
                .ToList();

            _context.StocksOnHold.RemoveRange(stocksOnHold);

            var order = new Order
            {
                OrderRef = CreateOrderRef(),
                StripeRef = request.StripeRef,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Address1 = request.Address1,
                Address2 = request.Address2,
                City = request.City,
                Postcode = request.Postcode,

                OrderStocks = request.Stocks.Select(x => new OrderStock
                {
                    StockId = x.StockId,
                    Qty = x.Qty
                }).ToList()
            };

            _context.Orders.Add(order);

            return await _context.SaveChangesAsync() > 0;

        }

        public string CreateOrderRef()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = new char[12];
            var random = new Random();

            //do while checks if the new string is same as existing and keeps running while this is true
            do
            {
                for (int i = 0; i < result.Length; i++)
                    result[i] = chars[random.Next(chars.Length)];
            } while (_context.Orders.Any(x => x.OrderRef == new string(result)));
                        
            return new string(result);
        }

        public class Request
        {
            public string StripeRef { get; set; }
            public string SessionId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string Postcode { get; set; }
            public List<Stock> Stocks { get; set; }

        }
        public class Stock
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
