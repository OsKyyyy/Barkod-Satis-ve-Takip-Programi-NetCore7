using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation.CustomerMovement;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.CustomerMovement;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;

namespace Business.Concrete
{
    [SecuredOperation("Admin")]
    public class CustomerMovementManager : ICustomerMovementService
    {
        private readonly CultureInfo _culture = new("en-US");

        private string Type { get; set; }

        ICustomerMovementDal _customerMovementDal;

        public CustomerMovementManager(ICustomerMovementDal customerMovementDal)
        {
            _customerMovementDal = customerMovementDal;
        }

        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(CustomerMovementAddDto customerMovementAddDto)
        {
            var customerMovement = new CustomerMovement
            {
                CustomerId = customerMovementAddDto.CustomerId,
                SaleId = customerMovementAddDto.SaleId,
                ProcessType = customerMovementAddDto.ProcessType,
                Amount = decimal.Parse(customerMovementAddDto.Amount, _culture),
                ProductInformation = customerMovementAddDto.ProductInformation,
                Note = customerMovementAddDto.Note,
                Status = customerMovementAddDto.Status,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = customerMovementAddDto.CreateUserId,
                UpdateUserId = customerMovementAddDto.CreateUserId,
                Deleted = false
            };
            _customerMovementDal.Add(customerMovement);

            return new SuccessResult(Messages.CustomerMovementAdded);
        }

        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(CustomerMovementUpdateDto customerMovementUpdateDto)
        {
            var customerMovement = new CustomerMovement
            {
                Id = customerMovementUpdateDto.Id,
                ProcessType = customerMovementUpdateDto.ProcessType,
                Amount = decimal.Parse(customerMovementUpdateDto.Amount, _culture),
                ProductInformation = customerMovementUpdateDto.ProductInformation,
                Note = customerMovementUpdateDto.Note,
                Status = customerMovementUpdateDto.Status,
                UpdateUserId = customerMovementUpdateDto.UpdateUserId,
                UpdateDate = DateTime.Now,
            };
            _customerMovementDal.Update(customerMovement);

            return new SuccessResult(Messages.CustomerMovementUpdated);
        }

        public IResult Delete(int id)
        {
            _customerMovementDal.Delete(id);
            return new SuccessResult(Messages.CustomerMovementDeleted);
        }

        public IResult DeleteBySaleId(int id)
        {
            _customerMovementDal.DeleteBySaleId(id);
            return new SuccessResult(Messages.CustomerMovementDeleted);
        }

        public IDataResult<ViewModel> ListById(int id)
        {
            var result = _customerMovementDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.CustomerMovementNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.CustomerMovementInfoListed);
        }

        public IDataResult<List<ViewModel>> ListByCustomerId(int id)
        {
            var result = _customerMovementDal.ListByCustomerId(id);

            return new SuccessDataResult<List<ViewModel>>(result, Messages.CustomerMovementInfoListed);
        }
    }
}
