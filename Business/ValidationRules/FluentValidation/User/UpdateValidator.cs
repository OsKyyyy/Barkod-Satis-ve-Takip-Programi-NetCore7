using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.ValidationRules.FluentValidation.User
{
    public class UpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Email).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Phone).Matches("^\\(?([1-9]{1})\\)?([0-9]{2})[. ]?([0-9]{3})[. ]?([0-9]{4})$").WithMessage("Bu alan uygun formatta değil. ( 5XXXXXXXXX )");
        }
    }
}
