using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Exceptions;

public class ApiExceptionFilter : ExceptionFilterAttribute
{
    public override Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is ApiException apiException)
        {
            context.Result = new ObjectResult(apiException.Message)
            {
                StatusCode = (int) apiException.Code
            };
        }
        
        return base.OnExceptionAsync(context);
    }
}