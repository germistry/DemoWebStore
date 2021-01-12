using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStore.Application.Orders;

namespace WebStore.UI.Pages.Customer
{
    public class OrderModel : PageModel
    {
        public GetOrder.Response Order { get; set; }
        public void OnGet(
            [FromServices] GetOrder getOrder,
            string orderRef)
        {
            Order = getOrder.Action(orderRef);
        }
    }
}
