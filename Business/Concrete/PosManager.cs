using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.Pos;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;

namespace Business.Concrete
{
    [SecuredOperation("Admin")]
    public class PosManager : IPosService
    {
        IPosDal _posDal;

        public PosManager(IPosDal posDal)
        {
            _posDal = posDal;
        }

        public IResult Add(PosAddDto posAddDto)
        {
            var pos = new Pos
            {
                Barcode = posAddDto.Barcode,
                ProductName = posAddDto.ProductName,
                ProductUnitPrice = posAddDto.ProductUnitPrice,
                ProductQuantity = posAddDto.ProductQuantity,
                CreateUserId = posAddDto.CreateUserId
            };
            _posDal.Add(pos);

            return new SuccessResult();
        }

        public IResult AddMoney(PosAddMoneyDto posAddDto)
        {
            var pos = new Pos
            {
                Barcode = "0",
                ProductName = "Extra Para",
                ProductUnitPrice = posAddDto.ProductUnitPrice,
                ProductQuantity = 1,
                CreateUserId = posAddDto.CreateUserId
            };
            _posDal.AddMoney(pos);

            return new SuccessResult();
        }

        public IDataResult<List<ViewModel>> List(int createUserId)
        {
            var result = _posDal.List(createUserId);

            return new SuccessDataResult<List<ViewModel>>(result, Messages.PosListed);
        }

        public IDataResult<ViewModel> ListById(int id)
        {
            var result = _posDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.PosNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.PosListed);
        }

        public IResult QuantityIncrease(int id)
        {
            _posDal.QuantityIncrease(id);

            return new SuccessResult(Messages.PosQuantityChange);
        } 
        
        public IResult QuantityReduce(int id)
        {
            _posDal.QuantityReduce(id);

            return new SuccessResult(Messages.PosQuantityChange);
        }

        public IResult Delete(int id)
        {
            _posDal.Delete(id);
            return new SuccessResult(Messages.PosDeleted);
        }
    }
}
