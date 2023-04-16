using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Fitness.Backend.Application.DataContracts.Exceptions;
using Fitness.Backend;

namespace Fitness.Backend.WebApi.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ResourceNotFoundException httpResponseException)
            {
                context.Result = new ObjectResult(httpResponseException.Message)
                {
                    StatusCode = 404
                };

                context.ExceptionHandled = true;
            }
            if (context.Exception is ResourceAlreadyExistsException e)
            {
                context.Result = new ObjectResult(e.Message)
                {
                    StatusCode = 400
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
