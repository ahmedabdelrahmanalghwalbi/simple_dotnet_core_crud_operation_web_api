namespace CrudOperations.middlewares
{
    public class RateLimitingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private static int _counter = 0;
        private static DateTime _lastRequestDateTime = DateTime.Now;

        public async Task Invoke(HttpContext context) 
        {
            _counter++;
            
            // IF THE CLIENT REQUEST AFTER 10 SECONDS AFTER LAST REQUEST
            if (DateTime.Now.Subtract(_lastRequestDateTime).Seconds > 10) {
                _counter = 1;
                _lastRequestDateTime = DateTime.Now;
                 await _next(context);
                return;
            }

            // IF THE CLIENT REQUEST LESS THAN 10 REQUESTS IN 10 SECONDS 
            if (_counter < 5)
            {
                _lastRequestDateTime =DateTime.Now;
                await _next(context);
            }
            else 
            {
                // IF THE CLIENT REQUEST MORE THAT 10 REQUESTS IN 10 SECONDS ( REACH RATE LIMIT !!!!!!! )
                _lastRequestDateTime =DateTime.Now;
                await context.Response.WriteAsync("RATE LIMITED EXCEED");
            
            }
            return;

        }
    }
}
