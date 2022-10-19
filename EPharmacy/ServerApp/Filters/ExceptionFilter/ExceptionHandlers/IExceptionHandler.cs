using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPharmacy.ServerApp.Filters.ExceptionFilter.ExceptionHandlers
{
    public interface IExceptionHandler
    {
        IActionResult Handle(HttpResponse response);
    }
}
