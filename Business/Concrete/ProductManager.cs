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
using Core.Utilities.Refit.Models.Response.Product;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.Concrete
{
    [SecuredOperation("Admin")]
    public class ProductManager : IProductService
    {
        private readonly CultureInfo _culture = new("en-US");

        private readonly IConfiguration Configuration;

        private readonly IProductDal _productDal;

        private string Type { get; set; }

        public ProductManager(IProductDal productDal, IConfiguration configuration)
        {
            _productDal = productDal;
            Configuration = configuration;
        }

        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(ProductAddDto productAddDto)
        {
            var imageUrl = "Uploads/Products/unknow.png";

            if (!string.IsNullOrEmpty(productAddDto.Image))
            {
                imageUrl = SaveFile(productAddDto.Image, productAddDto.Name);

                if (imageUrl == null)
                {
                    return new ErrorResult(Messages.ImagePropertyError);
                }
            }
            
            var product = new Product
            {
                CategoryId = productAddDto.CategoryId,
                Name = productAddDto.Name,
                PurchasePrice = decimal.Parse(productAddDto.PurchasePrice, _culture),
                SalePrice = decimal.Parse(productAddDto.SalePrice, _culture),
                PreviousSellingPrice = decimal.Parse(productAddDto.SalePrice, _culture),
                Barcode = productAddDto.Barcode,
                Stock = productAddDto.Stock,
                CriticalStock = productAddDto.CriticalStock,
                Image = imageUrl,
                Favorite = productAddDto.Favorite,
                Status = productAddDto.Status,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = productAddDto.CreateUserId,
                UpdateUserId = productAddDto.CreateUserId,
                Deleted = false
            };
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto.ImageChanged)
            {
                productUpdateDto.Image = SaveFile(productUpdateDto.Image, productUpdateDto.Name);

                if (productUpdateDto.Image == null)
                {
                    return new ErrorResult(Messages.ImagePropertyError);
                }
            }

            var category = new Product
            {
                Id = productUpdateDto.Id,
                CategoryId = productUpdateDto.CategoryId,
                Name = productUpdateDto.Name,
                PurchasePrice = decimal.Parse(productUpdateDto.PurchasePrice, _culture),
                SalePrice = decimal.Parse(productUpdateDto.SalePrice, _culture),
                Barcode = productUpdateDto.Barcode,
                Stock = productUpdateDto.Stock,
                CriticalStock = productUpdateDto.CriticalStock,
                Image = productUpdateDto.Image,
                Favorite = productUpdateDto.Favorite,
                Status = productUpdateDto.Status,
                UpdateDate = DateTime.Now,
                UpdateUserId = productUpdateDto.UpdateUserId
            };
            _productDal.Update(category);

            return new SuccessResult(Messages.ProductUpdated);
        }

        public IResult UpdateStock(string barcode, int quantity)
        {
            _productDal.UpdateStock(barcode, quantity);

            return new SuccessResult(Messages.ProductsListed);
        }

        public IResult UpdateAddStock(string barcode, int quantity)
        {
            _productDal.UpdateAddStock(barcode, quantity);

            return new SuccessResult(Messages.ProductsListed);
        }

        public IResult Delete(int id)
        {
            _productDal.Delete(id);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<List<ViewModel>> List()
        {
            var result = _productDal.List();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.ProductsListed);
        }

        public IDataResult<ViewModel> ListById(int id)
        {
            var result = _productDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.ProductNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.ProductInfoListed);
        }

        public IDataResult<List<ViewModel>> ListByName(string name)
        {
            var result = _productDal.ListByName(name);

            return new SuccessDataResult<List<ViewModel>>(result, Messages.ProductsListed);
        }

        public IDataResult<ViewModel> ListToPos(string barcode)
        {
            var result = _productDal.ListToPos(barcode);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.ProductNotFound);
            }

            if (result.Stock < 1)
            {
                return new ErrorDataResult<ViewModel>(Messages.ProductOutOfStock);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.ProductInfoListed);
        }

        public IDataResult<List<ViewModel>> ListByFavorite()
        {
            var result = _productDal.ListByFavorite();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.FavoriteProductsListed);
        }

        public IDataResult<ViewModel> CheckExistsByBarcode(string barcode)
        {
            var result = _productDal.CheckExistsByBarcode(barcode);
            if (result == null)
            {
                return new SuccessDataResult<ViewModel>(Messages.ProductNotFound);
            }

            return new ErrorDataResult<ViewModel>(result, Messages.ProductAlreadyExists);
        }

        private string? SaveFile(string? base64String, string productName)
        {
            var result = CheckFileProperty(base64String);

            if (!result)
            {
                return null;
            }

            var fullPath = "Uploads/Products/" + productName.GenerateSlug() + "-" + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + Type;
            File.WriteAllBytes(fullPath, Convert.FromBase64String(base64String));

            return fullPath;
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
