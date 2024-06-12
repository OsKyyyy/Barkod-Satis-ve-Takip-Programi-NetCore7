using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.ValidationRules.FluentValidation.IncomeAndExpenses
{
    public class UpdateValidator : AbstractValidator<IncomeAndExpensesTypeUpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(r => r.Id).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Name).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.UpdateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}
