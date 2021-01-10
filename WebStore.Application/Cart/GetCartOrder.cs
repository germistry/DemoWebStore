using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.Application.Infrastructure;
using WebStore.Database;

namespace WebStore.Application.Cart
{
    public class GetCartOrder
    {
        private readonly ISessionManager _sessionManager;
        private ApplicationDBContext _context;

        public GetCartOrder(ISessionManager sessionManager, ApplicationDBContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }

        public Response Action()
        {
            var cart = _sessionManager.GetCart();

            var listProducts = _context.Stock
                .Include(x => x.Product).AsEnumerable()
                .Where(x => cart.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Value = (int)(x.Product.Value * 100), 
                    Qty = cart.FirstOrDefault(y => y.StockId == x.Id).Qty
                })
                .ToList();

            var customerInfo = _sessionManager.GetCustomerInfo();

            return new Response 
            { 
                Products = listProducts,
                CustomerInfo = new CustomerInfo
                {
                    FirstName = customerInfo.FirstName,
                    LastName = customerInfo.LastName,
                    EmailAddress = customerInfo.EmailAddress,
                    PhoneNumber = customerInfo.PhoneNumber,
                    Address1 = customerInfo.Address1,
                    Address2 = customerInfo.Address2,
                    City = customerInfo.City,
                    Postcode = customerInfo.Postcode
                }
            };
        }
        public class Product
        {
            public int ProductId { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }
            public int Value { get; set; }
        }
        public class CustomerInfo
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string Postcode { get; set; }
        }
        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInfo CustomerInfo { get; set; }

            public int GetTotalCharge() => Products.Sum(x => x.Value * x.Qty);
        }
    }
}
