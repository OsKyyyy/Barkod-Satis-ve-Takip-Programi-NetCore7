﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.ValidationRules.FluentValidation.WholeSalerMovement
{
    public class AddValidator : AbstractValidator<WholeSalerMovementAddDto>
    {
        public AddValidator()
        {
            RuleFor(r => r.WholeSalerId).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.Amount).NotEmpty().WithMessage("Bu alan boş olamaz").Matches("^\\d{0,8}(\\.\\d{1,2})?$").WithMessage("Bu alan Decimal(10,2) formatında olmalıdır"); ;
            RuleFor(r => r.ProcessType).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.InvoiceDate).NotEmpty().WithMessage("Bu alan boş olamaz");
            RuleFor(r => r.CreateUserId).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}
