using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.OrdersAdmin
{
    [Service]
    public class UpdateOrder
    {
        private readonly IOrderManager _orderManager;
        public UpdateOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public Task Action(int id)
        {
            return _orderManager.AdvanceOrder(id);
        }
    }
}
