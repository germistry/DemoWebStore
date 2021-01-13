using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Application.Cart
{
    [Service]
    public class AddCustomerInfo
    {
        private readonly ISessionManager _sessionManager;

        public AddCustomerInfo(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public void Action(Request request)
        {
            _sessionManager.AddCustomerInfo(new CustomerInfo
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Address1 = request.Address1,
                Address2 = request.Address2,
                City = request.City,
                Postcode = request.Postcode
            });
        }

        public class Request
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
    }
}
