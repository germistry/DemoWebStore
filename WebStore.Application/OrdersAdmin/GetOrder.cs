using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.OrdersAdmin
{
    public class GetOrder
    {
        private readonly IOrderManager _orderManager;

        public GetOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public Response Action(int id) =>
            _orderManager.GetOrderById(id, x => new Response
            {
                Id = x.Id,
                OrderRef = x.OrderRef,
                StripeRef = x.StripeRef,
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailAddress = x.EmailAddress,
                PhoneNumber = x.PhoneNumber,
                Address1 = x.Address1,
                Address2 = x.Address2,
                City = x.City,
                Postcode = x.Postcode,

                Products = x.OrderStocks.Select(y => new Product
                {
                    Name = y.Stock.Product.Name,
                    Description = y.Stock.Product.Description,
                    Qty = y.Qty,
                    StockDescription = y.Stock.Description
                }),
                TotalValue = x.OrderStocks.Sum(y => y.Stock.Product.Value * y.Qty).ToString("N2")
            }); 

        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }
            public string StripeRef { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string Postcode { get; set; }
            public string TotalValue { get; set; }
            public IEnumerable<Product> Products { get; set; }
        }
        public class Product
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
            public string StockDescription { get; set; }

        }
    }
}
