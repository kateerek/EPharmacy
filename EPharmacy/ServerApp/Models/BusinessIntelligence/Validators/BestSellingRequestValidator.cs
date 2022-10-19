using EPharmacy.ServerApp.Models.BusinessIntelligence.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.BusinessIntelligence.Validators
{
    public class BestSellingRequestValidator : AbstractValidator<BestSellingRequest>
    {
        public BestSellingRequestValidator()
        {
            RuleFor(x => x.To)
                .GreaterThanOrEqualTo(x => x.From)
                .WithMessage("Data To jest mniejsza od Data From");
        }
    }
}
