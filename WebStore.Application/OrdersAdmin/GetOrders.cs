using System.Collections.Generic;
using System.Linq;
using WebStore.Database;
using WebStore.Domain.Enums;

namespace WebStore.Application.OrdersAdmin
{
    public class GetOrders
    {
        private ApplicationDBContext _context;
        public GetOrders(ApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Response> Action(int status) =>
            _context.Orders
                .Where(x => x.Status == (OrderStatus)status)
                .Select(x => new Response
                {
                    Id = x.Id,
                    OrderRef = x.OrderRef,
                    EmailAddress = x.EmailAddress
                })
                .ToList();
        
        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }
            public string EmailAddress { get; set; }
        }
    }
}
