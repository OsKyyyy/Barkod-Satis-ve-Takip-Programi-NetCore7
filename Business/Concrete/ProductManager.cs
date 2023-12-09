using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
           
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(ProductAddDto productAddDto)
        {
            var result = CheckFileProperty(productAddDto.Image);

            if (!result)
            {
                return new ErrorResult(Messages.ImagePropertyError);
            }
            
            var imageUrl = productAddDto.Name.GenerateSlug() + "-" + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            SaveFile(productAddDto.Image, imageUrl);

            var product = new Product
            {
                CategoryId = productAddDto.CategoryId,
                Name = productAddDto.Name,
                PurchasePrice = decimal.Parse(productAddDto.PurchasePrice, _culture),
                SalePrice = decimal.Parse(productAddDto.SalePrice, _culture),
                Barcode = productAddDto.Barcode,
                Stock = productAddDto.Stock,
                Image = imageUrl,
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

        private void SaveFile(string base64String, string imageUrl)
        {
            File.WriteAllBytes(@"Uploads\Products", Convert.FromBase64String(base64String));
        }

        protected static bool CheckFileProperty(string? base64String)
        {
            if (base64String == null)
            {
                return true;
            }
            /*
             * START CHECK SIZE
             */

            var length = base64String.AsSpan()[(base64String.IndexOf(',') + 1)..].Length;
            var fileSizeInByte = Math.Ceiling((double)length / 4) * 3;

            double byteToMegaByte = (fileSizeInByte / 1024f) / 1024f;

            if (byteToMegaByte > 2) // 2MB
            {
                return false;
            }

            /*
             * END CHECK SIZE
             */

            /*
             * START CHECK TYPE
             */

            var type = base64String[..5];

            return type == "iVBOR" || type.ToUpper() == "/9J/4";

            /*
             * END CHECK TYPE
             */
        }
    }
}
