using System.Diagnostics;

namespace مشروع_قبل_الشغل.Middlewares
{
    public class ProfilingMideelware(RequestDelegate next,ILogger<ProfilingMideelware> logger)
    {
        private readonly RequestDelegate next = next;
        private readonly ILogger<ProfilingMideelware> logger = logger;

        public async Task Invoke(HttpContext context) 
        { 
            var Stopwatch = new Stopwatch();
            Stopwatch.Start();
            await next(context);
            Stopwatch.Stop();
            logger.LogInformation($"Request {context.Request.Path} Took {Stopwatch.ElapsedMilliseconds} to Exeuted");
        }
    }
}
