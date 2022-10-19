using AutoMapper;
using EPharmacy.ServerApp.Exceptions;
using EPharmacy.ServerApp.Filters.ExceptionFilter.ExceptionHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EPharmacy.ServerApp.Filters.ExceptionFilter
{
    public static class ExceptionHandlersProvider
    {
        public static IReadOnlyDictionary<Type, Func<Exception, IServiceProvider, IExceptionHandler>> ExceptionToHandlerFactory { get; } = new Dictionary<Type, Func<Exception, IServiceProvider, IExceptionHandler>>
            {
                { typeof(ValidationException), (e, s) => CreateHandler(e as ValidationException, s) },
                { typeof(NotFoundException), (e, s) => CreateHandler(e as NotFoundException, s)},
                { typeof(InternalServerErrorException), (e, s) => CreateHandler(e as InternalServerErrorException, s)},
                { typeof(Exception), (e, s) => CreateHandler(e, s)}
            };

        public static IExceptionHandler CreateHandler(ValidationException exception, IServiceProvider services)
        {
            var (logger, mapper) = GetServices<ValidationExceptionHandler>(services);
            return new ValidationExceptionHandler(exception, logger, mapper);
        }

        public static IExceptionHandler CreateHandler(NotFoundException exception, IServiceProvider services)
        {
            var (logger, mapper) = GetServices<NotFoundExceptionHandler>(services);
            return new NotFoundExceptionHandler(exception, logger, mapper);
        }

        public static IExceptionHandler CreateHandler(InternalServerErrorException exception, IServiceProvider services)
        {
            var (logger, mapper) = GetServices<InternalServerErrorExceptionHandler>(services);
            return new InternalServerErrorExceptionHandler(exception, logger, mapper);
        }

        public static IExceptionHandler CreateHandler(Exception exception, IServiceProvider services)
        {
            var logger = services.GetService<ILogger<UndefinedExceptionHandler>>();
            var localizer = services.GetService<IStringLocalizer<UndefinedExceptionHandler>>();
            return new UndefinedExceptionHandler(exception, logger, localizer);
        }

        public static (ILogger<T> logger, IMapper mapper) GetServices<T>(IServiceProvider services)
        {
            return (services.GetService<ILogger<T>>(), services.GetService<IMapper>());
        }
    }
}
