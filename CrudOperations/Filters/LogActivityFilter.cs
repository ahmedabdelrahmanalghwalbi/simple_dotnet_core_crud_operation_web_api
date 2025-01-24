using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CrudOperations.Filters
{
    public class LogActivityFilter : IActionFilter
    {
        private ILogger<LogActivityFilter> _logger;

        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.Result = new OkObjectResult ("Hello from OnActionExecuting");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //_logger.LogInformation("Hello from OnActionExecuted");
             
        }

    }
}
