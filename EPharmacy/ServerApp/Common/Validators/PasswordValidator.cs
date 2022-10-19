using FluentValidation.Resources;
using FluentValidation.Validators;
using Microsoft.Extensions.Localization;
using System;
using System.Text.RegularExpressions;

namespace EPharmacy.ServerApp.Common.Validators
{
    public class PasswordValidator : PropertyValidator, IRegularExpressionValidator
    {
        private readonly Regex _regex;

        const string _expression = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$";

        public PasswordValidator(IStringLocalizer<PasswordValidator> localizer)
            : base(new StaticStringSource(localizer.GetString("BadPasswordFormat")))
        {
            _regex = CreateRegEx();
        }


        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null) return true;

            if (!_regex.IsMatch((string)context.PropertyValue))
            {
                return false;
            }

            return true;
        }

        public string Expression => _expression;

        private static Regex CreateRegEx()
        {
            try
            {
                if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
                {
                    return new Regex(_expression, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2.0));
                }
            }
            catch
            {
            }
            return new Regex(_expression, RegexOptions.IgnoreCase);
        }
    }
}
