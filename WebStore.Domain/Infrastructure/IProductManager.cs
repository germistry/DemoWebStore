﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Models;

namespace WebStore.Domain.Infrastructure
{
    public interface IProductManager
    {
        Task<int> CreateProduct(Product product);
        Task<int> DeleteProduct(int id);
        Task<int> UpdateProduct(Product product);
        TResult GetProductById<TResult>(int id, Func<Product, TResult> selector);
        TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStockTop6<TResult>(Func<Product, TResult> selector);
    }
}
