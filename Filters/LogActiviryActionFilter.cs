using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace مشروع_قبل_الشغل.Filters
{
    public class LogActiviryActionFilter : IActionFilter
    {
        private readonly ILogger<LogActiviryActionFilter> logger;

        public LogActiviryActionFilter(ILogger<LogActiviryActionFilter> logger)
        {
            this.logger = logger;
        }
        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation($"Executed Action {context.ActionDescriptor.DisplayName} in Controller{context.Controller} with {JsonSerializer.Serialize(context.ActionArguments)}");
        }
        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation($"Executed Action {context.ActionDescriptor.DisplayName} in Controller{context.Controller}"); 
        }

        
    }
}
