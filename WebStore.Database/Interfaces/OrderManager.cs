﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebStore.Domain.Enums;
using WebStore.Domain.Infrastructure;
using WebStore.Domain.Models;

namespace WebStore.Database.Interfaces
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDBContext _context;

        public OrderManager(ApplicationDBContext context)
        {
            _context = context;
        }
        public bool OrderRefExists(string orderRef)
        {
            return _context.Orders.Any(x => x.OrderRef == orderRef);
        }
        private TResult GetOrder<TResult>(Expression<Func<Order, bool>> condition, Expression<Func<Order, TResult>> selector)
        {
            return _context.Orders
                .Where(condition)
                .Include(x => x.OrderStocks)
                    .ThenInclude(x => x.Stock)
                        .ThenInclude(x => x.Product)
                .Select(selector)
                .FirstOrDefault();
        }
        public TResult GetOrderByRef<TResult>(string orderRef, Expression<Func<Order, TResult>> selector)
        {
            return GetOrder(order => order.OrderRef == orderRef, selector);
        }
        public TResult GetOrderById<TResult>(int id, Expression<Func<Order, TResult>> selector)
        {
            return GetOrder(order => order.Id == id, selector); 
        }
        public IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus orderStatus, Func<Order, TResult> selector)
        {
            return _context.Orders
                .Where(x => x.Status == orderStatus)
                .Select(selector)
                .ToList();
        }
        public Task<int> CreateOrder(Order order)
        {
            _context.Orders.Add(order);

            return _context.SaveChangesAsync();
        }
        public Task AdvanceOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            order.Status = order.Status + 1;

            return _context.SaveChangesAsync(); 
        }
    }
}
