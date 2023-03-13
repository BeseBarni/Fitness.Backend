using Microsoft.AspNetCore.Mvc.Filters;

namespace Fitness.Backend.WebApi.Filters
{
    public class ResourceNotFoundExceptionFilterAttribute : ExceptionFilterAttribute 
    {
        public override void OnException(ExceptionContext context)
        {
            
        }
    }
}
