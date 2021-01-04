using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStore.Application.Orders;
using WebStore.Database;

namespace WebStore.UI.Pages.Customer
{
    public class OrderModel : PageModel
    {
        private ApplicationDBContext _context;

        public OrderModel(ApplicationDBContext context)
        {
            _context = context;
        }

        public GetOrder.Response Order { get; set; }
        public void OnGet(string orderRef)
        {
            Order = new GetOrder(_context).Action(orderRef);
        }
    }
}
