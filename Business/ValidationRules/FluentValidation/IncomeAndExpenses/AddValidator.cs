using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.ValidationRules.FluentValidation.IncomeAndExpenses
{
    public class AddValidator : AbstractValidator<IncomeAndExpensesAddDto>
    {
        public AddValidator()
        {
            RuleFor(r => r.IncomeExpensesTypeId).NotEmpty().WithMessage("Bu alan boş olamaz").GreaterThan(0).WithMessage("Bu alan 0'dan büyük değer olmalıdır");
            RuleFor(r => r.Amount).NotEmpty().WithMessage("Bu alan boş olamaz").Matches("^\\d{0,8}(\\.\\d{1,2})?$").WithMessage("Bu alan Decimal(10,2) formatında olmalıdır"); ;
            RuleFor(r => r.CreateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}
