using EPharmacy.ServerApp.Common;
using EPharmacy.ServerApp.Common.Validators;
using FluentValidation;

namespace EPharmacy.ServerApp.Extensions
{
    public static class RuleBuilderExtension
    {
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new PasswordValidator(StaticServiceProvider.GetLocalizer<PasswordValidator>()));
        }
    }
}
