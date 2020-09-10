using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Core.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace forgeSampleAPI_DotNetCore.Core.Extensions
{
    public static class ExceptionHandlerMiddleware
    {
        public static void ConfigureToCustomExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<HttpErrorHandlerMiddleware>();
        }
    }
}
