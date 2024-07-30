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
using Core.Utilities.Refit.Models.Request.Product;
using Core.Utilities.Refit.Models.Response.Product;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;

namespace Business.Concrete
{    
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

        [SecuredOperation("product_add")]
        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(ProductAddDto productAddDto)
        {
            var imageUrl = "Uploads/Products/unknow.png";

            if (!string.IsNullOrEmpty(productAddDto.Image))
            {
                imageUrl = SaveFile(productAddDto.Image, productAddDto.Barcode);

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

        [SecuredOperation("product_stockentry")]
        [ValidationAspect(typeof(StockEntryValidator))]
        public IResult StockEntry(StockEntryRequestModel stockEntryRequestModel)
        {                        
            _productDal.StockEntry(stockEntryRequestModel);

            return new SuccessResult(Messages.ProductStockEntry);
        }

        [SecuredOperation("product_add")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto.ImageChanged)
            {
                productUpdateDto.Image = SaveFile(productUpdateDto.Image, productUpdateDto.Barcode);

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

        [SecuredOperation("product_add")]
        public IResult UpdateStock(string barcode, int quantity)
        {
            _productDal.UpdateStock(barcode, quantity);

            return new SuccessResult(Messages.ProductsListed);
        }

        [SecuredOperation("product_add")]
        public IResult UpdateAddStock(string barcode, int quantity)
        {
            _productDal.UpdateAddStock(barcode, quantity);

            return new SuccessResult(Messages.ProductsListed);
        }

        [SecuredOperation("product_list")]
        public IResult Delete(int id)
        {
            _productDal.Delete(id);
            return new SuccessResult(Messages.ProductDeleted);
        }

        [SecuredOperation("product_list")]
        public IDataResult<List<ViewModel>> List()
        {
            var result = _productDal.List();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.ProductsListed);
        }

        [SecuredOperation("product_list")]
        public IDataResult<ViewModel> ListById(int id)
        {
            var result = _productDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.ProductNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.ProductInfoListed);
        }

        [SecuredOperation("product_list")]
        public IDataResult<List<ViewModel>> ListByName(string name)
        {
            var result = _productDal.ListByName(name);

            return new SuccessDataResult<List<ViewModel>>(result, Messages.ProductsListed);
        }

        [SecuredOperation("product_list")]
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

        [SecuredOperation("product_list")]
        public IDataResult<List<ViewModel>> ListByFavorite()
        {
            var result = _productDal.ListByFavorite();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.FavoriteProductsListed);
        }

        [SecuredOperation("product_list")]
        public IDataResult<List<ViewModel>> ListByCategory(int id)
        {
            var result = _productDal.ListByCategory(id);

            return new SuccessDataResult<List<ViewModel>>(result, Messages.CategoryProductsListed);
        }

        [SecuredOperation("product_list")]
        public IDataResult<ViewModel> CheckExistsByBarcode(string barcode)
        {
            var result = _productDal.CheckExistsByBarcode(barcode);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.ProductNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.ProductAlreadyExists);
        }

        [SecuredOperation("product_list")]
        public IResult CheckExistsByBarcodeAndId(int id, string barcode)
        {
            var result = _productDal.CheckExistsByBarcodeAndId(id, barcode);

            if (result)
            {
                return new ErrorResult(Messages.ProductAlreadyExists);
            }

            return new SuccessResult();
        }

        [SecuredOperation("product_list")]
        public bool ListToSavePhoto(string barcode)
        {
            var result = _productDal.ListToSavePhoto(barcode);

            return result;
        }

        [SecuredOperation("product_add")]
        public IResult UpdateImage(UpdateImageRequestModel updateImageRequestModel)
        {                      
            _productDal.UpdateImage(updateImageRequestModel);

            return new SuccessResult(Messages.ProductUpdated);
        }

        private string? SaveFile(string? base64String, string barcode)
        {
            var result = CheckFileProperty(base64String);

            if (!result)
            {
                return null;
            }

            var fullPath = "Uploads/Products/" + barcode + "_" + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + Type;
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
