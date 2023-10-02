using Microsoft.AspNetCore.Mvc.Filters;

namespace DotnetCoreVCB.Common.Filter
{
    public class JwtAuthenticationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
