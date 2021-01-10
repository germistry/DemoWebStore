﻿using System.ComponentModel.DataAnnotations;
using WebStore.Application.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Application.Cart
{
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
