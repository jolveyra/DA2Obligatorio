using ManagementApi.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManagementApi.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException) 
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message }) { StatusCode = 400 };
            } else if (context.Exception is ResourceNotFoundException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message }) { StatusCode = 404 };
            } else if (context.Exception is Exception)
            {
                // TODO: ELIMINAR EL TIPO DE EXCEPTION
                context.Result = new ObjectResult(new { ErrorMessage = $"Something went wrong. See: {context.Exception.GetType()} {context.Exception.Message}" }) { StatusCode = 500 };
            }
        }
    }
}
