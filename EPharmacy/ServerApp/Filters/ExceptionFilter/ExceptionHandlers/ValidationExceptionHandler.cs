using System;
using System.Net;
using AutoMapper;
using EPharmacy.ServerApp.Exceptions;
using EPharmacy.ServerApp.Models.Common.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EPharmacy.ServerApp.Filters.ExceptionFilter.ExceptionHandlers
{
    public class ValidationExceptionHandler : BaseExceptionHandlerWithMapper
    {
        private readonly ValidationException _exception;

        public ValidationExceptionHandler(ValidationException exception, ILogger<ValidationExceptionHandler> logger, IMapper mapper)
            : base(logger, mapper)
        {
            _exception = exception;
        }

        protected override void LogExceptionInformation(LogLevel logLevel, Exception exception)
        {
            base.LogExceptionInformation(logLevel, exception);
            try
            {
                var failuresJSON = Newtonsoft.Json.JsonConvert.SerializeObject(_exception.Failures);
                _logger.Log(logLevel, failuresJSON);
            }
            catch (Exception serializationException)
            {
                _logger.Log(logLevel, $"{serializationException}");
            }
        }

        public override IActionResult Handle(HttpResponse response)
        {
            LogExceptionInformation(LogLevel.Debug, _exception);
            SetUpResponseHeaders(response);
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new JsonResult(_mapper.Map<BadRequestResponse>(_exception));
        }
    }
}
