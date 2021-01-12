using System.Collections.Generic;
using WebStore.Domain.Enums;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.OrdersAdmin
{
    public class GetOrders
    {
        private readonly IOrderManager _orderManager;
        public GetOrders(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public IEnumerable<Response> Action(int orderStatus) =>
            _orderManager.GetOrdersByStatus((OrderStatus)orderStatus, x => new Response
            {
                Id = x.Id,
                OrderRef = x.OrderRef,
                EmailAddress = x.EmailAddress
            });
            
        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }
            public string EmailAddress { get; set; }
        }
    }
}
