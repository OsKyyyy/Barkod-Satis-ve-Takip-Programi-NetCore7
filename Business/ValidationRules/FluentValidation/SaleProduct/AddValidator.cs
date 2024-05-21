using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.ValidationRules.FluentValidation.SaleProduct
{
    public class AddValidator : AbstractValidator<SaleProductAddDto>
    {
        public AddValidator()
        {
            RuleFor(r => r.SaleId).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.ProductName).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.ProductUnitPrice).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}
