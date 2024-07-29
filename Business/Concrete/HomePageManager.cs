using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation.Category;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.HomePage;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;

namespace Business.Concrete
{    
    public class HomePageManager : IHomePageService
    {
        IProductDal _productDal;
        ISaleDal _saleDal;
        IUserDal _userDal;
        ICustomerDal _customerDal;
        IWholeSalerDal _wholeSalerDal;

        public HomePageManager(IProductDal productDal, ISaleDal saleDal, IUserDal userDal, ICustomerDal customerDal, IWholeSalerDal wholeSalerDal)
        {
            _productDal = productDal;
            _saleDal = saleDal;
            _userDal = userDal;
            _customerDal = customerDal;
            _wholeSalerDal = wholeSalerDal;
        }

        public IDataResult<StockQuantityViewModel> GetStockQuantity()
        {
            var result = _productDal.GetStockQuantity();

            return new SuccessDataResult<StockQuantityViewModel>(result, Messages.StockQuantityListed);
        }

        public IDataResult<StockValueViewModel> GetStockValue()
        {
            var result = _productDal.GetStockValue();

            return new SuccessDataResult<StockValueViewModel>(result, Messages.StockValueListed);
        }

        public IDataResult<SalesTodayViewModel> GetSalesToday()
        {
            var result = _saleDal.GetSalesToday();

            return new SuccessDataResult<SalesTodayViewModel>(result, Messages.SalesTodayListed);
        }

        public IDataResult<TotalCurrentAccountViewModel> GetTotalUser()
        {
            var result = _userDal.GetTotalUser();

            return new SuccessDataResult<TotalCurrentAccountViewModel>(result, Messages.TotalUserListed);
        }

        public IDataResult<TotalCurrentAccountViewModel> GetTotalCustomer()
        {
            var result = _customerDal.GetTotalCustomer();

            return new SuccessDataResult<TotalCurrentAccountViewModel>(result, Messages.TotalCustomerListed);
        }

        public IDataResult<TotalCurrentAccountViewModel> GetTotalWholeSaler()
        {
            var result = _wholeSalerDal.GetTotalWholeSaler();

            return new SuccessDataResult<TotalCurrentAccountViewModel>(result, Messages.TotalWholeSalerListed);
        }
    }
}
