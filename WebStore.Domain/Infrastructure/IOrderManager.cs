﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebStore.Domain.Enums;
using WebStore.Domain.Models;

namespace WebStore.Domain.Infrastructure
{
    public interface IOrderManager
    {
        bool OrderRefExists(string orderRef);
        TResult GetOrderByRef<TResult>(string orderRef, Expression<Func<Order, TResult>> selector);
        TResult GetOrderById<TResult>(int id, Expression<Func<Order, TResult>> selector);
        IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus orderStatus, Func<Order, TResult> selector);
        Task<int> CreateOrder(Order order);
        Task AdvanceOrder(int id);
    }
}
