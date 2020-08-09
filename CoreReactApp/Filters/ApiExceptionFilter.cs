using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreReactApp.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.HttpContext.Request.Path.HasValue 
                && context.HttpContext.Request.Path.Value.Contains("api"))
            {
                var e = context.Exception;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }
                context.Result = new BadRequestObjectResult(new { errorText = e.Message });
                context.ExceptionHandled = true;
            }
        }
    }
}
