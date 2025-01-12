using System.Diagnostics;

namespace CrudOperations.middlewares
{
    public class ProfilingMiddleware(RequestDelegate next, ILogger<ProfilingMiddleware> logger)
    {

        private readonly ILogger<ProfilingMiddleware> _logger = logger;
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context) 
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();
            _logger.LogInformation($"This Request {context.Request.Path} took {stopwatch.ElapsedMilliseconds} time ");
        }
    }
}
