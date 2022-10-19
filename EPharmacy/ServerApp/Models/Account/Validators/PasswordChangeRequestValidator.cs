using FluentValidation;
using EPharmacy.ServerApp.Models.Account.Requests;
using EPharmacy.ServerApp.Extensions;
using Microsoft.Extensions.Localization;

namespace EPharmacy.ServerApp.Models.Account.Validators
{
    public class PasswordChangeRequestValidator : AbstractValidator<PasswordChangeRequest>
    {
        public PasswordChangeRequestValidator(IStringLocalizer<PasswordChangeRequestValidator> localizer)
        {
            RuleFor(x => x.CurrentPassword)
                .Password();
            RuleFor(x => x.NewPassword)
                .Password()
                .NotEqual(x => x.CurrentPassword).WithMessage(localizer.GetString("PasswordDiff"));
        }
    }
}
