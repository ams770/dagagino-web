using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dagagino.Middlewares
{
    public class RateLimitMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private DateTime _lastReqTime = DateTime.Now;
        private int counter = 0;

        public async Task Invoke(HttpContext context)
        {
            counter++;
            /* ------------------------- Check Last Request Time ------------------------ */
            /* -------- if more that 10 seconds reset counter and perform request ------- */
            if (DateTime.Now.Subtract(_lastReqTime).Seconds > 10)
            {
                counter = 1;
                _lastReqTime = DateTime.Now;
                await _next(context);
            }
            else
            {
                /* ------------------------- Check Number of Requests within 10 Seconds ------------------------ */
                /* ------------------------ if exceeded 5 block user ------------------------ */
                if (counter > 5)
                {
                    _lastReqTime = DateTime.Now;
                    await context.Response.WriteAsync("Rate Limit Exceeded");
                }
                else
                {
                    _lastReqTime = DateTime.Now;
                    await _next(context);
                }
            }
        }
    }
}