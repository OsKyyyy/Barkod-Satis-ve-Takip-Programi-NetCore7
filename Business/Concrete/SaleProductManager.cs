using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation.SaleProduct;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.SaleProduct;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;

namespace Business.Concrete
{
    [SecuredOperation("Admin")]
    public class SaleProductManager : ISaleProductService
    {
        private readonly CultureInfo _culture = new("en-US");

        ISaleProductDal _saleProductDal;        

        public SaleProductManager(ISaleProductDal saleProductDal)
        {
            _saleProductDal = saleProductDal;            
        }

        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(SaleProductAddDto saleAddDto)
        {
            var saleProduct = new SaleProduct
            {
                SaleId = saleAddDto.SaleId,
                Barcode = saleAddDto.Barcode,
                ProductName = saleAddDto.ProductName,
                ProductUnitPrice = saleAddDto.ProductUnitPrice,
                ProductQuantity = saleAddDto.ProductQuantity,
                Deleted = false
            };
            
            _saleProductDal.Add(saleProduct);

            return new SuccessResult(Messages.SaleAdded);
        }

        public IDataResult<List<ViewModel>> ListById(int id)
        {
            var result = _saleProductDal.ListById(id);

            return new SuccessDataResult<List<ViewModel>>(result, Messages.SaleProductsDetailReportListed);
        }

    }
}
