using AutoMapper;
using Microsoft.Extensions.Logging;

namespace EPharmacy.ServerApp.Filters.ExceptionFilter.ExceptionHandlers
{
    public abstract class BaseExceptionHandlerWithMapper : BaseExceptionHandler
    {
        protected readonly IMapper _mapper;
        protected BaseExceptionHandlerWithMapper(ILogger<BaseExceptionHandlerWithMapper> logger, IMapper mapper)
            : base(logger)
        {
            _mapper = mapper;
        }
    }
}
