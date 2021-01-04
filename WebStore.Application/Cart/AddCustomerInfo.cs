using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Models;

namespace WebStore.Application.Cart
{
    public class AddCustomerInfo
    {
        private ISession _session;

        public AddCustomerInfo(ISession session)
        {
            _session = session;
        }

        public void Action(Request request)
        {
            var customerInfo = new CustomerInfo
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Address1 = request.Address1,
                Address2 = request.Address2,
                City = request.City,
                Postcode = request.Postcode
            };

            var stringObject = JsonConvert.SerializeObject(customerInfo);
            
            _session.SetString("customer-info", stringObject);
        }

        public class Request
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string EmailAddress { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            [Required]
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string Postcode { get; set; }
        }
    }
}
