﻿using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.Orders
{
    [Service]
    public class GetOrder
    {
        private readonly IOrderManager _orderManager;
        public GetOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public Response Action(string orderRef) =>
            _orderManager.GetOrderByRef(orderRef, x => new Response
            {
                OrderRef = x.OrderRef,
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
                    StockValue = $"$ {y.Stock.StockValue}",
                    Qty = y.Qty,
                    StockName = y.Stock.StockName
                }),
                TotalValue = x.OrderStocks.Sum(y => y.Stock.StockValue * y.Qty).ToString("N2")
            });
        

        public class Response
        {
            public string OrderRef { get; set; }
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
            public string StockValue { get; set; }
            public int Qty { get; set; }
            public string StockName { get; set; }
        }
    }
}
