using System.Linq;
using System.Threading.Tasks;
using WebStore.Database;

namespace WebStore.Application.OrdersAdmin
{
    public class UpdateOrder
    {
        private ApplicationDBContext _context;
        public UpdateOrder(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> ActionAsync(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            order.Status = order.Status + 1;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
