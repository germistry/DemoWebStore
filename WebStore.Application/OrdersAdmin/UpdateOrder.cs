using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.OrdersAdmin
{
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
