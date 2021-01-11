using System.Threading.Tasks;
using WebStore.Domain.Models;

namespace WebStore.Domain.Infrastructure
{
    public interface IStockManager
    {
        Stock GetStockWithProduct(int stockId);
        bool EnoughStock(int stockId, int qty);
        Task PutStockOnHold(int stockId, int qty, string sessionId);
        Task RemoveStockOnHold(int stockId, int qty, string sessionId);  
    }
}
