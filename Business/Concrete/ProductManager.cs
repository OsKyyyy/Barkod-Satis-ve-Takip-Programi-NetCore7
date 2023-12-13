using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation.Product;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    [SecuredOperation("Admin")]
    public class ProductManager : IProductService
    {
        private readonly CultureInfo _culture = new("en-US");

        private readonly IProductDal _productDal;

        private string Type { get; set; }

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(ProductAddDto productAddDto)
        {
            if (!string.IsNullOrEmpty(productAddDto.Image))
            {
                var imageUrl = SaveFile(productAddDto.Image, productAddDto.Name);

                if (imageUrl == null)
                {
                    return new ErrorResult(Messages.ImagePropertyError);
                }

                productAddDto.Image = imageUrl;
            }
            
            var product = new Product
            {
                CategoryId = productAddDto.CategoryId,
                Name = productAddDto.Name,
                PurchasePrice = decimal.Parse(productAddDto.PurchasePrice, _culture),
                SalePrice = decimal.Parse(productAddDto.SalePrice, _culture),
                Barcode = productAddDto.Barcode,
                Stock = productAddDto.Stock,
                Image = productAddDto.Image,
                Favorite = productAddDto.Favorite,
                Status = productAddDto.Status,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = productAddDto.CreateUserId,
                UpdateUserId = productAddDto.CreateUserId,
            };
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        private string? SaveFile(string? base64String, string productName)
        {
            var result = CheckFileProperty(base64String);

            if (!result)
            {
                return null;
            }

            var imageUrl = productName.GenerateSlug() + "-" + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + Type;

            var fullPath = @"D:\Test\" + imageUrl;
            File.WriteAllBytes(fullPath, Convert.FromBase64String(base64String));

            return imageUrl;
        }

        protected bool CheckFileProperty(string? base64String)
        {
            var length = base64String.AsSpan()[(base64String.IndexOf(',') + 1)..].Length;
            var fileSizeInByte = Math.Ceiling((double)length / 4) * 3;

            var byteToMegaByte = (fileSizeInByte / 1024f) / 1024f;

            if (byteToMegaByte > 2)
            {
                return false;
            }

            var type = base64String[..5];

            switch (type)
            {
                case "iVBOR":
                    Type = ".png";
                    return true;

                case "/9j/4":
                    Type = ".jpg";
                    return true;

                default:
                    return false;
            }
        }
    }
}
