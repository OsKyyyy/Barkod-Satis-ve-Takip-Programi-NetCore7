using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.ValidationRules.FluentValidation.IncomeAndExpenses
{
    public class AddValidator : AbstractValidator<IncomeAndExpensesTypeAddDto>
    {
        public AddValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.CreateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}
