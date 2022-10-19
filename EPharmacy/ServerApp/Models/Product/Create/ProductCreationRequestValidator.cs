using EPharmacy.ServerApp.Models.Discounts.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Product.Create
{
    public class ProductCreationRequestValidator : AbstractValidator<ProductCreationRequest>
    {
        public ProductCreationRequestValidator()
        {
            RuleFor(x => x.PrescriptionDiscounts)
                .SetCollectionValidator(new PrescriptionDiscountModelValidator());
        }
    }
}
