using System.Net;
using AutoMapper;
using EPharmacy.ServerApp.Exceptions;
using EPharmacy.ServerApp.Models.Common.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EPharmacy.ServerApp.Filters.ExceptionFilter.ExceptionHandlers
{
    public class NotFoundExceptionHandler : BaseExceptionHandlerWithMapper
    {
        private readonly NotFoundException _exception;

        public NotFoundExceptionHandler(NotFoundException exception, ILogger<NotFoundExceptionHandler> logger, IMapper mapper)
            : base(logger, mapper)
        {
            _exception = exception;
        }

        public override IActionResult Handle(HttpResponse response)
        {
            LogExceptionInformation(LogLevel.Debug, _exception);
            SetUpResponseHeaders(response);
            response.StatusCode = (int)HttpStatusCode.NotFound;
            return new JsonResult(_mapper.Map<NotFoundResponse>(_exception));
        }
    }
}
