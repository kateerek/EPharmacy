using EPharmacy.ServerApp.Common;
using Microsoft.Extensions.Localization;
using System;

namespace EPharmacy.ServerApp.Exceptions
{
    public class NotFoundException : Exception
    {
        public string ErrorMessage { get; }
        public NotFoundException(string name, object key, IStringLocalizer<NotFoundException> localizer = null)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
            if(localizer == null)
            {
                localizer = StaticServiceProvider.GetLocalizer<NotFoundException>();
            }
            ErrorMessage = localizer.GetString("ErrorMessage", new object[] { name, key });
        }
    }
}
