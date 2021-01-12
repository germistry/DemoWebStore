using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Models;

namespace WebStore.Domain.Infrastructure
{
    public interface IStockManager
    {
        Task<int> CreateStock(Stock stock);
        Task<int> DeleteStock(int id);
        Task<int> UpdateStockRange(List<Stock> stocks);

        Stock GetStockWithProduct(int stockId);
        bool EnoughStock(int stockId, int qty);
        Task PutStockOnHold(int stockId, int qty, string sessionId);
        Task RemoveStockOnHold(int stockId, int qty, string sessionId);
        Task RemoveStockOnHold(string sessionId);
        Task ActionExpiredStockOnHold();
    }
}
