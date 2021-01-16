using Microsoft.AspNetCore.Http;
using System.IO;

namespace WebStore.Domain.Infrastructure
{
    public interface IFileManager
    {
        string SaveProductImage(IFormFile productImage);
        FileStream ProductImageStream(string productImage);
        void RemoveProductImage(string productImage);
    }
}
