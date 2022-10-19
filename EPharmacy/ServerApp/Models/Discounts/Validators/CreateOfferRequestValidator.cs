using EPharmacy.ServerApp.Models.Discounts.Models;
using EPharmacy.ServerApp.Models.Discounts.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts.Validators
{
    public class CreateOfferRequestValidator : AbstractValidator<CreateOfferRequest>
    {
        public CreateOfferRequestValidator()
        {
            RuleFor(x => x.DiscountValue)
                .SetValidator(new DiscountValueModelValidator());
            RuleFor(x => x.ValidTo)
                .GreaterThan(DateTime.Now)
                .WithMessage($"Data trawania zniżki musi być większa od daty dzisiejszej: {DateTime.Now}");
            RuleFor(x => x.Products)
                .NotNull()
                .WithMessage($"Lista produktów do zniżki jest wymagana");
            RuleFor(x => x.Attributes)
                .NotNull()
                .WithMessage($"Lista attrybutów do zniżki jest wymagana");
        }
    }
}
