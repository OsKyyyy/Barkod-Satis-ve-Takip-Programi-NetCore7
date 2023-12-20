using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.ValidationRules.FluentValidation.Product
{
    public class UpdateValidator : AbstractValidator<ProductUpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Bu alan boş olamaz").GreaterThan(0).WithMessage("Bu alan 0'dan büyük değer olmalıdır"); ;
            RuleFor(r => r.CategoryId).NotEmpty().WithMessage("Bu alan boş olamaz").GreaterThan(0).WithMessage("Bu alan 0'dan büyük değer olmalıdır"); ;
            RuleFor(r => r.Name).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.PurchasePrice).NotEmpty().WithMessage("Bu alan boş olamaz").Matches("^\\d{0,8}(\\.\\d{1,2})?$").WithMessage("Bu alan Decimal(10,2) formatında olmalıdır");
            RuleFor(r => r.SalePrice).NotEmpty().WithMessage("Bu alan boş olamaz").Matches("^\\d{0,8}(\\.\\d{1,2})?$").WithMessage("Bu alan Decimal(10,2) formatında olmalıdır"); ;
            RuleFor(r => r.Barcode).NotEmpty().WithMessage("Bu alan boş olamaz").Matches("^([0 - 9]\\d|\\d{8,14})$").WithMessage("Bu alan en az 8 en fazla 14 karakter olmalıdır"); ;
            RuleFor(r => r.Stock).NotEmpty().WithMessage("Bu alan boş olamaz").GreaterThan(0).WithMessage("Bu alan 0'dan büyük değer olmalıdır");
            RuleFor(r => r.UpdateUserId).NotEmpty().WithMessage("Bu alan boş olamaz").GreaterThan(0).WithMessage("Bu alan 0'dan büyük değer olmalıdır"); ;
        }
    }
}
