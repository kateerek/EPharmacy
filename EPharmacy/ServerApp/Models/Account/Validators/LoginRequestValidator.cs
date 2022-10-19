using FluentValidation;
using EPharmacy.ServerApp.Models.Account.Requests;
using EPharmacy.ServerApp.Extensions;
using Microsoft.Extensions.Localization;
using EPharmacy.ServerApp.Services.Account;

namespace EPharmacy.ServerApp.Models.Account.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator(IStringLocalizer<LoginRequestValidator> localizer, IAccountService accountService)
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .MustAsync(async (email, ct) => await accountService.UserExists(email))
                .WithMessage(localizer.GetString("UserNotExist"));
            RuleFor(x => x.Password)
                .Password();
        }
    }
}