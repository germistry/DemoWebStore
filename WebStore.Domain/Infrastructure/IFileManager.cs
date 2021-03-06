﻿using Microsoft.AspNetCore.Http;
using System.IO;

namespace WebStore.Domain.Infrastructure
{
    public interface IFileManager
    {
        string SaveProductImage(IFormFile productImage);
        FileStream ProductImageStream(string productImage);
        FileStream ProductLogoImageStream(string productLogoImage);
        void RemoveProductImage(string productImage);

        string SaveCategoryImage(IFormFile categoryImage);
        FileStream CategoryImageStream(string categoryImage);
        void RemoveCategoryImage(string categoryImage);
    }
}
