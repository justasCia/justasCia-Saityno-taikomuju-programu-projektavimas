using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BallTalkAPI.Filters
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is BadHttpRequestException exception)
            {
                context.Result = exception.StatusCode switch
                {
                    StatusCodes.Status404NotFound => new NotFoundObjectResult(exception.Message),
                    _ => new BadRequestResult(),
                };
            }
        }
    }
}
