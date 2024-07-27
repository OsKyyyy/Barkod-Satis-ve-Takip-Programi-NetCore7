using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation.Customer;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.Customer;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;

namespace Business.Concrete
{    
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [SecuredOperation("customer_add")]
        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(CustomerAddDto customerAddDto)
        {
            var customer = new Customer
            {
                Name = customerAddDto.Name,
                Address = customerAddDto.Address,
                Phone = customerAddDto.Phone,
                Email = customerAddDto.Email,
                Status = customerAddDto.Status,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = customerAddDto.CreateUserId,
                UpdateUserId = customerAddDto.CreateUserId,
                Deleted = false
            };
            _customerDal.Add(customer);

            return new SuccessResult(Messages.CustomerAdded);
        }

        [SecuredOperation("customer_add")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(CustomerUpdateDto customerUpdateDto)
        {
            var customer = new Customer
            {
                Id = customerUpdateDto.Id,
                Name = customerUpdateDto.Name,
                Address = customerUpdateDto.Address,
                Phone = customerUpdateDto.Phone,
                Email = customerUpdateDto.Email,
                Status = customerUpdateDto.Status,
                UpdateUserId = customerUpdateDto.UpdateUserId,
                UpdateDate = DateTime.Now,
            };
            _customerDal.Update(customer);

            return new SuccessResult(Messages.CustomerUpdated);
        }

        [SecuredOperation("customer_list")]
        public IDataResult<List<ViewModel>> List()
        {
            var result = _customerDal.List();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.CustomersListed);
        }

        [SecuredOperation("customer_list")]
        public IDataResult<List<ViewModel>> ListActive()
        {
            var result = _customerDal.ListActive();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.CustomersListed);
        }

        [SecuredOperation("customer_list")]
        public IResult Delete(int id)
        {
            _customerDal.Delete(id);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        [SecuredOperation("customer_list")]
        public IDataResult<ViewModel> ListById(int id)
        {
            var result = _customerDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.CustomerNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.CustomerInfoListed);
        }
    }
}
