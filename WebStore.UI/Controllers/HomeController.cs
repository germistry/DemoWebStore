using Microsoft.AspNetCore.Mvc;
using WebStore.Application.Products;
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

        [HttpGet]
        public IActionResult GetCategoryNav([FromServices] GetCategoryMenu getCategoryMenu)
        {
            var categorymenu = getCategoryMenu.Action();
            return PartialView("Components/CategoryMenu/Default", categorymenu);
        }

        [HttpGet("/ProductImage/{productImage}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        //HttpGet to return the product image through filestream.
        public IActionResult ProductImage(string productImage) =>
           new FileStreamResult(_fileManager.ProductImageStream(productImage),
               $"productImage/{productImage.Substring(productImage.LastIndexOf('.') + 1)}");


        [HttpGet("/ProductLogoImage/{productLogoImage}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult ProductLogoImage(string productLogoImage) =>
            new FileStreamResult(_fileManager.ProductLogoImageStream(productLogoImage),
                $"productLogoImage/{productLogoImage.Substring(productLogoImage.LastIndexOf('.') + 1)}");

        [HttpGet("/CategoryImage/{categoryImage}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        //HttpGet to return the product image through filestream.
        public IActionResult CategoryImage(string categoryImage) =>
           new FileStreamResult(_fileManager.CategoryImageStream(categoryImage),
               $"categoryImage/{categoryImage.Substring(categoryImage.LastIndexOf('.') + 1)}");

    }
}
