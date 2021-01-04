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
    public class GetOrder
    {
        private ISession _session;
        private ApplicationDBContext _context;

        public GetOrder(ISession session, ApplicationDBContext context)
        {
            _session = session;
            _context = context;
        }

        public Response Action()
        {
            var cart = _session.GetString("cart");
            
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cart);

            var listProducts = _context.Stock
                .Include(x => x.Product).AsEnumerable()
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Value = (int)(x.Product.Value * 100), 
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty
                })
                .ToList();

            var custInfoString = _session.GetString("customer-info");
            var customerInfo = JsonConvert.DeserializeObject<WebStore.Domain.Models.CustomerInfo>(custInfoString);

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
