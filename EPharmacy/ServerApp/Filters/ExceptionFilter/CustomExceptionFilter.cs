using Microsoft.AspNetCore.Mvc.Filters;
using System;
using EPharmacy.ServerApp.Filters.ExceptionFilter.ExceptionHandlers;
using System.Collections.Generic;

namespace EPharmacy.ServerApp.Filters.ExceptionFilter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private IReadOnlyDictionary<Type, Func<Exception, IServiceProvider, IExceptionHandler>> _handlers = ExceptionHandlersProvider.ExceptionToHandlerFactory;

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var handler = _handlers.GetValueOrDefault(exception.GetType(), _handlers[typeof(Exception)]);
            context.Result = handler(exception, context.HttpContext.RequestServices)
                                    .Handle(context.HttpContext.Response);
            base.OnException(context);
        }
    }
}