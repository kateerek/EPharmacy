using EPharmacy.ServerApp.Extensions;
using EPharmacy.ServerApp.Models.Account.Requests;
using FluentValidation;

namespace EPharmacy.ServerApp.Models.Account.Validators
{
    public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();
            RuleFor(x => x.Password)
                .Password();
            RuleFor(x => x.FirstName)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}
