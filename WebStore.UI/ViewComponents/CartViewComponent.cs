using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Application.Cart;
using WebStore.Database;

namespace WebStore.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private ApplicationDBContext _context;
        public CartViewComponent(ApplicationDBContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {
            return View(view, new GetCart(HttpContext.Session, _context).Action());
        }
    }
}
