using EPharmacy.ServerApp.Models.Discounts.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Discounts.Validators
{
    public class UpdateOfferRequestValidator : AbstractValidator<UpdateOfferRequest>
    {
        public UpdateOfferRequestValidator()
        {
            Include(new CreateOfferRequestValidator());
        }
    }
}
