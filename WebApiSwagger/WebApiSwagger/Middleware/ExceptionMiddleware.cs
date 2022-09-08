using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSwagger.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
         
                Console.WriteLine("Before Request In Placed");

                await next(context);

                Console.WriteLine("After Request Out Placed");

          
        }
    }
}
