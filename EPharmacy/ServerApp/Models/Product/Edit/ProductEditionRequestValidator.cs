using EPharmacy.ServerApp.Models.Product.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Product.Edit
{
    public class ProductEditionRequestValidator : AbstractValidator<ProductEditionRequest>
    {
        public ProductEditionRequestValidator()
        {
            Include(new ProductCreationRequestValidator());
        }
    }
}
