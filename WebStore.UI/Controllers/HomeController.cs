using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Infrastructure;

namespace WebStore.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IFileManager _fileManager;
        public HomeController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        
        [HttpGet("/ProductImage/{productImage}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        //HttpGet to return the product image through filestream.
        public IActionResult ProductImage(string productImage) =>
           new FileStreamResult(_fileManager.ProductImageStream(productImage),
               $"productImage/{productImage.Substring(productImage.LastIndexOf('.') + 1)}");
    }
}
