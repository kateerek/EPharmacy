using EPharmacy.ServerApp.Models.Discounts.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts.Validators
{
    public class DiscountValueModelValidator : AbstractValidator<DiscountValueModel>
    {
        public DiscountValueModelValidator()
        {
            RuleFor(x => x.DiscountType)
                .IsInEnum()
                .WithMessage("Wybrano niepoprawny typ zniżki");
            RuleFor(x => x)
                .Must(x => HaveProperPercenentValue(x))
                .WithMessage("Wprowadzono niepoprawny procent zniżki");
        }
        private bool HaveProperPercenentValue(DiscountValueModel x)
        {
            if (x.DiscountType == DiscountValueModel.DiscountValueType.Percent)
            {
                return 0 <= x.Value && x.Value <= 1;
            }
            return true;
        }
    }
}
