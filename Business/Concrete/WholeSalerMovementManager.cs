using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation.WholeSalerMovement;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Refit.Models.Response.WholeSalerMovement;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;

namespace Business.Concrete
{
    [SecuredOperation("wholesaler_list")]
    public class WholeSalerMovementManager : IWholeSalerMovementService
    {
        private readonly CultureInfo _culture = new("en-US");

        private string Type { get; set; }

        IWholeSalerMovementDal _wholeSalerMovementDal;

        public WholeSalerMovementManager(IWholeSalerMovementDal wholeSalerMovementDal)
        {
            _wholeSalerMovementDal = wholeSalerMovementDal;
        }

        [ValidationAspect(typeof(AddValidator))]
        public IResult Add(WholeSalerMovementAddDto wholeSalerMovementAddDto)
        {
            var imageUrl = "Invoices/unknow.png";

            if (!string.IsNullOrEmpty(wholeSalerMovementAddDto.Image))
            {
                imageUrl = SaveFile(wholeSalerMovementAddDto.Image, wholeSalerMovementAddDto.WholeSalerId);

                if (imageUrl == null)
                {
                    return new ErrorResult(Messages.ImagePropertyError);
                }
            }

            var wholeSalerMovement = new WholeSalerMovement
            {
                WholeSalerId = wholeSalerMovementAddDto.WholeSalerId,
                ProcessType = wholeSalerMovementAddDto.ProcessType,
                Amount = decimal.Parse(wholeSalerMovementAddDto.Amount, _culture),
                InvoiceDate = wholeSalerMovementAddDto.InvoiceDate,
                Note = wholeSalerMovementAddDto.Note,
                Image = imageUrl,
                Status = wholeSalerMovementAddDto.Status,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateUserId = wholeSalerMovementAddDto.CreateUserId,
                UpdateUserId = wholeSalerMovementAddDto.CreateUserId,
                Deleted = false
            };
            _wholeSalerMovementDal.Add(wholeSalerMovement);

            return new SuccessResult(Messages.WholeSalerMovementAdded);
        }

        [ValidationAspect(typeof(UpdateValidator))]
        public IResult Update(WholeSalerMovementUpdateDto wholeSalerMovementUpdateDto)
        {
            if (wholeSalerMovementUpdateDto.ImageChanged)
            {
                wholeSalerMovementUpdateDto.Image = SaveFile(wholeSalerMovementUpdateDto.Image, wholeSalerMovementUpdateDto.WholeSalerId);

                if (wholeSalerMovementUpdateDto.Image == null)
                {
                    return new ErrorResult(Messages.ImagePropertyError);
                }
            }

            var wholeSalerMovement = new WholeSalerMovement
            {
                Id = wholeSalerMovementUpdateDto.Id,
                ProcessType = wholeSalerMovementUpdateDto.ProcessType,
                Amount = decimal.Parse(wholeSalerMovementUpdateDto.Amount, _culture),
                InvoiceDate = wholeSalerMovementUpdateDto.InvoiceDate,
                Note = wholeSalerMovementUpdateDto.Note,
                Image = wholeSalerMovementUpdateDto.Image,
                Status = wholeSalerMovementUpdateDto.Status,
                UpdateUserId = wholeSalerMovementUpdateDto.UpdateUserId,
                UpdateDate = DateTime.Now,
            };
            _wholeSalerMovementDal.Update(wholeSalerMovement);

            return new SuccessResult(Messages.WholeSalerMovementUpdated);
        }

        public IResult Delete(int id)
        {
            _wholeSalerMovementDal.Delete(id);
            return new SuccessResult(Messages.WholeSalerMovementDeleted);
        }

        public IDataResult<ViewModel> ListById(int id)
        {
            var result = _wholeSalerMovementDal.ListById(id);
            if (result == null)
            {
                return new ErrorDataResult<ViewModel>(Messages.WholeSalerMovementNotFound);
            }

            return new SuccessDataResult<ViewModel>(result, Messages.WholeSalerMovementInfoListed);
        }

        public IDataResult<List<ViewModel>> ListByWholeSalerId(int id)
        {
            var result = _wholeSalerMovementDal.ListByWholeSalerId(id);

            return new SuccessDataResult<List<ViewModel>>(result, Messages.WholeSalerMovementInfoListed);
        }

        private string? SaveFile(string? base64String, int productName)
        {
            var result = CheckFileProperty(base64String);

            if (!result)
            {
                return null;
            }

            var fullPath = "Uploads/Invoices/" + productName + "-" + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + Type;
            File.WriteAllBytes(fullPath, Convert.FromBase64String(base64String));

            fullPath = fullPath.Replace("Uploads/", "");

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
                
                case "JVBER":
                    Type = ".pdf";
                    return true;

                default:
                    return false;
            }
        }

    }
}
