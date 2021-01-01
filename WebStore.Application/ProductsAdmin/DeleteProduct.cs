﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Database;

namespace WebStore.Application.ProductsAdmin
{
    public class DeleteProduct
    {
        private ApplicationDBContext _context;

        public DeleteProduct(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<bool> Action(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
