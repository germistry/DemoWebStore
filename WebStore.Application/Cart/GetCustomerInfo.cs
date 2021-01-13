using WebStore.Domain.Infrastructure;

namespace WebStore.Application.Cart
{
    [Service]
    public class GetCustomerInfo
    {
        private readonly ISessionManager _sessionManager;

        public GetCustomerInfo(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public Response Action()
        {
            var customerInfo = _sessionManager.GetCustomerInfo();

            if (customerInfo == null)
                return null;
            
            return new Response 
            {
                FirstName = customerInfo.FirstName,
                LastName = customerInfo.LastName,
                EmailAddress = customerInfo.EmailAddress,
                PhoneNumber = customerInfo.PhoneNumber,
                Address1 = customerInfo.Address1,
                Address2 = customerInfo.Address2,
                City = customerInfo.City,
                Postcode = customerInfo.Postcode
            };
        }

        public class Response
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
