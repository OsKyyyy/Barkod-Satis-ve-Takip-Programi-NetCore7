using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation.WholeSaler;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.WholeSaler;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;

namespace Business.Concrete
{    
    public class WholeSalerManager : IWholeSalerService
    {
        IWholeSalerDal _wholeSalerDal;

        public WholeSalerManager(IWholeSalerDal wholeSalerDal)
        {
            _wholeSalerDal = wholeSalerDal;
        }

        [SecuredOperation("wholesaler_add")]
        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(WholeSalerAddDto wholeSalerAddDto)
        {
            var wholeSaler = new WholeSaler
            {
                Name = wholeSalerAddDto.Name,
                AuthorizedPerson = wholeSalerAddDto.AuthorizedPerson,
                Phone = wholeSalerAddDto.Phone,
                Email = wholeSalerAddDto.Email,
                Address = wholeSalerAddDto.Address,
                Status = wholeSalerAddDto.Status,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = wholeSalerAddDto.CreateUserId,
                UpdateUserId = wholeSalerAddDto.CreateUserId,
                Deleted = false
            };
            _wholeSalerDal.Add(wholeSaler);

            return new SuccessResult(Messages.WholeSalerAdded);
        }

        [SecuredOperation("wholesaler_add")]
        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(WholeSalerUpdateDto wholeSalerUpdateDto)
        {
            var wholeSaler = new WholeSaler
            {
                Id = wholeSalerUpdateDto.Id,
                Name = wholeSalerUpdateDto.Name,
                AuthorizedPerson = wholeSalerUpdateDto.AuthorizedPerson,
                Phone = wholeSalerUpdateDto.Phone,
                Email = wholeSalerUpdateDto.Email,
                Address = wholeSalerUpdateDto.Address,
                Status = wholeSalerUpdateDto.Status,
                UpdateUserId = wholeSalerUpdateDto.UpdateUserId,
                UpdateDate = DateTime.Now,
            };
            _wholeSalerDal.Update(wholeSaler);

            return new SuccessResult(Messages.CategoryUpdated);
        }

        [SecuredOperation("wholesaler_list")]
        public IDataResult<List<ViewModel>> List()
        {
            var result = _wholeSalerDal.List();

            return new SuccessDataResult<List<ViewModel>>(result, Messages.WholeSalerListed);
        }

        [SecuredOperation("wholesaler_list")]
        public IResult Delete(int id)
        {
            _wholeSalerDal.Delete(id);
            return new SuccessResult(Messages.WholeSalerDeleted);
        }

        [SecuredOperation("wholesaler_list")]
        public IDataResult<ViewModel> ListById(int id)
        {
            var result = _wholeSalerDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.WholeSalerNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.WholeSalerInfoListed);
        }

    }
}
