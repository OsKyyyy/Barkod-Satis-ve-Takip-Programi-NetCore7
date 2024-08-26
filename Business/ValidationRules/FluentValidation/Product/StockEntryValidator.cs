using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;
using Core.Utilities.Refit.Models.Request.Product;

namespace Business.ValidationRules.FluentValidation.Product
{
    public class StockEntryValidator : AbstractValidator<StockEntryRequestModel>
    {
        public StockEntryValidator()
        {
            RuleFor(r => r.UpdateUserId).NotEmpty().WithMessage("Bu alan boş olamaz").GreaterThan(0).WithMessage("Bu alan 0'dan büyük değer olmalıdır");
            RuleForEach(r => r.Product).SetValidator(new ProductValidator());
        }
    }
    public class ProductValidator : AbstractValidator<StockEntryProductsRequestModel>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Barcode)
                .NotEmpty().WithMessage("Bu alan boş olamaz")
                .Matches("^([0-9]\\d{7,13})$").WithMessage("Bu alan en az 8 en fazla 14 karakter olmalıdır");

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage("Bu alan boş olamaz")
                .GreaterThan(0).WithMessage("Bu alan 0'dan büyük bir değer olmalıdır");
        }
    }
}
