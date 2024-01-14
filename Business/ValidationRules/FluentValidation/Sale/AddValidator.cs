using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.ValidationRules.FluentValidation.Sale
{
    public class AddValidator : AbstractValidator<SaleAddDto>
    {
        public AddValidator()
        {
            RuleFor(r => r.Amount).NotEmpty().WithMessage("Bu alan boş olamaz").Matches("^\\d{0,8}(\\.\\d{1,2})?$").WithMessage("Bu alan Decimal(10,2) formatında olmalıdır"); ;
            RuleFor(r => r.PaymentType).NotEmpty();
            RuleFor(r => r.CreateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}
