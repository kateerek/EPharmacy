using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace EPharmacy.ServerApp.Filters.ExceptionFilter.ExceptionHandlers
{
    public abstract class BaseExceptionHandler : IExceptionHandler
    {
        protected readonly ILogger<BaseExceptionHandler> _logger;

        protected BaseExceptionHandler(ILogger<BaseExceptionHandler> logger)
        {
            _logger = logger;
        }

        protected virtual void SetUpResponseHeaders(HttpResponse response)
        {
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        protected virtual void LogExceptionInformation(LogLevel logLevel, Exception exception)
        {
            _logger.Log(logLevel, $"{exception}");
        }

        public abstract IActionResult Handle(HttpResponse response);
    }
}
