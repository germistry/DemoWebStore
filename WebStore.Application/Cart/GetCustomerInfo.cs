using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Models;

namespace WebStore.Application.Cart
{
    public class GetCustomerInfo
    {
        private ISession _session;

        public GetCustomerInfo(ISession session)
        {
            _session = session;
        }

        public Response Action()
        {
            var stringObject = _session.GetString("customer-info");

            if (String.IsNullOrEmpty(stringObject))
                return null;

            var customerInfo = JsonConvert.DeserializeObject<CustomerInfo>(stringObject);
            
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
