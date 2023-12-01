using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.ValidationRules.FluentValidation.User
{
    public class LoginValidator : AbstractValidator<UserLoginDto>
    {
        public LoginValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Bu alan boş olamaz").Must(r => r.Length > 8).WithMessage("Bu alan en az 8 karakter olmalıdır");
        }
    }
}
