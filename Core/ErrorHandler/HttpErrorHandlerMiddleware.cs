using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Autodesk.Forge;
using Autodesk.Forge.Client;
using forgeSampleAPI_DotNetCore.Core.ErrorHandler;

namespace forgeSampleAPI_DotNetCore.Core.Middlewares
{
    public class HttpErrorHandlerMiddleware
    {
        private RequestDelegate _next;

        public HttpErrorHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (ApiException e)
            {
                await HandleForgeAPIExceptionAsync(context, e);
            }


        }

        private Task HandleForgeAPIExceptionAsync(HttpContext context, ApiException e)
        {
            context.Response.ContentType = "application/json";
            string message = "";
            dynamic errorContent = null;

            if (e.GetType() == typeof(ApiException))
            {
                context.Response.StatusCode = e.ErrorCode;
                message = e.Message;
                errorContent = e.ErrorContent;

            }

            var response = context.Response.WriteAsync(new ForgeApiErrorDetails
            {
                errorContent = errorContent,
                message = message,
                code = context.Response.StatusCode
            }.ToString());

            return response;
        }
    }
}
