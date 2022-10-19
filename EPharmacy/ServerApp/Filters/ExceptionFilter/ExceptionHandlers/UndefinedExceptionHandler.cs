using System;
using EPharmacy.ServerApp.Models.Common.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EPharmacy.ServerApp.Filters.ExceptionFilter.ExceptionHandlers
{
    public class UndefinedExceptionHandler : BaseExceptionHandler
    {
        private readonly IStringLocalizer<UndefinedExceptionHandler> _localizer;
        private readonly Exception _exception;

        public UndefinedExceptionHandler(Exception exception, ILogger<UndefinedExceptionHandler> logger, IStringLocalizer<UndefinedExceptionHandler> localizer)
            :base(logger)
        {
            _localizer = localizer;
            _exception = exception;
        }

        public override IActionResult Handle(HttpResponse response)
        {
            LogExceptionInformation(LogLevel.Error, _exception);
            SetUpResponseHeaders(response);
            return new JsonResult(new InternalServerErrorResponse
            {
                ErrorMessage = _localizer.GetString("UndefinedExceptionErrorMessage").Value
            });
        }
    }
}
