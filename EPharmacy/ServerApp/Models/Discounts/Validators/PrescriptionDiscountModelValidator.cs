using EPharmacy.ServerApp.Models.Discounts.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts.Validators
{
    public class PrescriptionDiscountModelValidator : AbstractValidator<PrescriptionDiscountModel>
    {
        public PrescriptionDiscountModelValidator()
        {
            RuleFor(x => x.DiscountValue)
                .SetValidator(new DiscountValueModelValidator());
        }
    }
}
