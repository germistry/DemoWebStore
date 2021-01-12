using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Database.Interfaces
{
    public class StockManager : IStockManager
    {
        private readonly ApplicationDBContext _context;

        public StockManager(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task<int> CreateStock(Stock stock)
        {
            _context.Stock.Add(stock);
            return _context.SaveChangesAsync();
        }
        public Task<int> DeleteStock(int id)
        {
            var stock = _context.Stock.FirstOrDefault(x => x.Id == id);
            _context.Stock.Remove(stock);

            return _context.SaveChangesAsync();
        }
        public Task<int> UpdateStockRange(List<Stock> stocks)
        {
            _context.Stock.UpdateRange(stocks);
            return _context.SaveChangesAsync();
        }

        public Stock GetStockWithProduct(int stockId)
        {
            return _context.Stock
                    .Include(x => x.Product).AsEnumerable()
                    .FirstOrDefault(x => x.Id == stockId);
        }
        public bool EnoughStock(int stockId, int qty)
        {
            return _context.Stock
                    .FirstOrDefault(x => x.Id == stockId)
                    .Qty >= qty;
        }
        public Task PutStockOnHold(int stockId, int qty, string sessionId)
        {
            _context.Stock.FirstOrDefault(x => x.Id == stockId).Qty -= qty;

            var stockOnHold = _context.StocksOnHold
                .Where(x => x.SessionId == sessionId)
                .ToList();
            if (stockOnHold.Any(x => x.StockId == stockId))
            {
                stockOnHold.Find(x => x.StockId == stockId).Qty += qty;
            }
            else
            {
                _context.StocksOnHold.Add(new StockOnHold
                {
                    StockId = stockId,
                    SessionId = sessionId,
                    Qty = qty,
                    ExpiryDate = DateTime.Now.AddMinutes(30)
                });
            }
            foreach (var stock in stockOnHold)
            {
                stock.ExpiryDate = DateTime.Now.AddMinutes(30);
            }
            return _context.SaveChangesAsync();
        }
        public Task RemoveStockOnHold(int stockId, int qty, string sessionId)
        {
            var stockOnHold = _context.StocksOnHold
                .FirstOrDefault(x => x.StockId == stockId && x.SessionId == sessionId);

            var stock = _context.Stock.FirstOrDefault(x => x.Id == stockId);

            stock.Qty += qty;
            stockOnHold.Qty -= qty;

            if (stockOnHold.Qty <= 0)
            {
                _context.Remove(stockOnHold);
            }

            return _context.SaveChangesAsync();
        }
        public Task RemoveStockOnHold(string sessionId)
        {
            var stocksOnHold = _context.StocksOnHold
                 .Where(x => x.SessionId == sessionId)
                 .ToList();

            _context.StocksOnHold.RemoveRange(stocksOnHold);
            return _context.SaveChangesAsync();
        }
        public Task ActionExpiredStockOnHold()
        {
            var stocksOnHold = _context.StocksOnHold
                .Where(x => x.ExpiryDate < DateTime.Now)
                .ToList();
            if (stocksOnHold.Count > 0)
            {
                var stocksToReturn = _context.Stock
                    .AsEnumerable()
                    .Where(x => stocksOnHold
                        .Any(y => y.StockId == x.Id))
                    .ToList();
                foreach (var stock in stocksToReturn)
                {
                    stock.Qty = stock.Qty + stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id).Qty;
                }
                _context.StocksOnHold.RemoveRange(stocksOnHold);

                return _context.SaveChangesAsync();
            }
            return Task.CompletedTask;
        }
    }
}
