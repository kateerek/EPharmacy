using AutoMapper;
using EPharmacy.ServerApp.Exceptions;
using EPharmacy.ServerApp.Models.Common.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EPharmacy.ServerApp.Filters.ExceptionFilter.ExceptionHandlers
{
    public class InternalServerErrorExceptionHandler : BaseExceptionHandlerWithMapper
    {
        private readonly InternalServerErrorException _exception;

        public InternalServerErrorExceptionHandler(InternalServerErrorException exception, ILogger<InternalServerErrorExceptionHandler> logger, IMapper mapper)
            :base(logger, mapper)
        {
            _exception = exception;
        }

        public override IActionResult Handle(HttpResponse response)
        {
            LogExceptionInformation(LogLevel.Debug, _exception);
            SetUpResponseHeaders(response);
            return new JsonResult(_mapper.Map<InternalServerErrorResponse>(_exception));
        }
    }
}
