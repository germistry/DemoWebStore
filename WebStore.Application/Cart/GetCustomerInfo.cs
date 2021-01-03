using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Application.Cart
{
    public class GetCustomerInfo
    {
        private ISession _session;

        public GetCustomerInfo(ISession session)
        {
            _session = session;
        }

        public Request Action()
        {
            var stringObject = _session.GetString("customer-info");

            if (String.IsNullOrEmpty(stringObject))
                return null;

            var response = JsonConvert.DeserializeObject<Request>(stringObject);
            return response;
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
