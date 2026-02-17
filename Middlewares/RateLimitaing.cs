namespace مشروع_قبل_الشغل.Middlewares
{
    public class RateLimitaing(RequestDelegate next, ILogger<RateLimitaing> logger)
    {
        private readonly RequestDelegate next = next;
        private readonly ILogger<RateLimitaing> logger = logger;
        private static int Count = 0;
        private static DateTime lastRquest= DateTime.Now;

        public async Task Invoke(HttpContext context) 
        {
            Count++;
            if (DateTime.Now.Subtract(lastRquest).Seconds > 10)
            {
                Count = 1;
                lastRquest= DateTime.Now;
                await next(context);
            }
            else if(Count > 5)
            {
                lastRquest = DateTime.Now;
                await context.Response.WriteAsync("Rate Limit Exeected");
            }
            else
            {
                lastRquest = DateTime.Now;
                await next(context);
            }
            
        }
    }
}
