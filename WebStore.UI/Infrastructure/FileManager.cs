using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PhotoSauce.MagicScaler;
using System;
using System.IO;
using WebStore.Domain.Infrastructure;

namespace WebStore.UI.Infrastructure
{
    public class FileManager : IFileManager
    {
        private readonly string _productImagePath;
        private readonly string _productLogoImagePath;

        public FileManager(IConfiguration config)
        {
            _productImagePath = config["Path:ProductImages"];
            _productLogoImagePath = config["Path:ProductLogoImages"];
        }
        private ProcessImageSettings ProductImageOptions() => new ProcessImageSettings
        {
            Width = 660,
            Height = 495,
            ResizeMode = CropScaleMode.Crop,
            SaveFormat = FileFormat.Jpeg,
            JpegQuality = 100,
            JpegSubsampleMode = ChromaSubsampleMode.Subsample420
        };
        private ProcessImageSettings LogoImageOptions() => new ProcessImageSettings
        {
            Width = 48,
            Height = 48,
            ResizeMode = CropScaleMode.Crop,
            SaveFormat = FileFormat.Jpeg,
            JpegQuality = 100,
            JpegSubsampleMode = ChromaSubsampleMode.Subsample420
        };
        public FileStream ProductImageStream(string productImage)
        {
            return new FileStream(Path.Combine(_productImagePath, productImage), FileMode.Open, FileAccess.Read);
        }

        public void RemoveProductImage(string productImage)
        {
            try
            {
                var productFile = Path.Combine(_productImagePath, productImage);
                if (File.Exists(productFile))
                    File.Delete(productFile);
                
                var logoFile = Path.Combine(_productLogoImagePath, productImage);
                if (File.Exists(logoFile))
                    File.Delete(logoFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //Creates the product main image and a second 48x48 logo image.
        public string SaveProductImage(IFormFile productImage)
        {
            try
            {
                //Get the path & if path can't be saved because doesn't exist then create it
                var productSavePath = Path.Combine(_productImagePath);
                if (!Directory.Exists(productSavePath))
                    Directory.CreateDirectory(productSavePath);
                var logoSavePath = Path.Combine(_productLogoImagePath);
                if (!Directory.Exists(logoSavePath))
                    Directory.CreateDirectory(logoSavePath);
                //Get the mime & fileName, done this way to prevent internet explorer errors
                var mime = productImage.FileName.Substring(productImage.FileName.LastIndexOf('.'));
                //Generate just the file name so it can be copied over to asset files instead of generic tag
                string fileWithExt = productImage.FileName;
                string[] file = fileWithExt.Split('.');
                string fileNoExt = file[0];

                var productFileName = $"{fileNoExt}_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";
                var logoFileName = $"{fileNoExt}_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";
                //Get the filestream and then save the image
                using (var productFileStream = new FileStream(Path.Combine(productSavePath, productFileName), FileMode.Create))
                {
                    //Imagine processing to make files smaller, same size etc doesnt have an async method
                    MagicImageProcessor.ProcessImage(productImage.OpenReadStream(), productFileStream, ProductImageOptions());
                }
                using (var fileStream = new FileStream(Path.Combine(logoSavePath, logoFileName), FileMode.Create))
                {
                    //Imagine processing to make files smaller, same size etc doesnt have an async method
                    MagicImageProcessor.ProcessImage(productImage.OpenReadStream(), fileStream, LogoImageOptions());
                }
                return productFileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }
    }
}
