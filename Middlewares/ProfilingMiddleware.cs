using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

/* -------------------------------------------------------------------------- */
/*                          Calculating Request Time                          */
/* -------------------------------------------------------------------------- */
namespace Dagagino.Middlewares
{
    public class ProfilingMiddleware(RequestDelegate next, ILogger<ProfilingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ProfilingMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context){
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();
            _logger.LogInformation($"Request `{context.Request.Path}` took `{stopwatch.ElapsedMilliseconds}`ms");

        }
    }
}