using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;

namespace WebStore.Application.StockAdmin
{
    public class DeleteStock
    {
        private readonly IStockManager _stockManager;

        public DeleteStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public Task<int> Action(int id)
        {
            return _stockManager.DeleteStock(id);
        }
    }
}
