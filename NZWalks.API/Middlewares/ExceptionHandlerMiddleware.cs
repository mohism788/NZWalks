﻿using System.Linq.Expressions;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddlewareNew
    {
        private readonly ILogger logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddlewareNew(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorID = Guid.NewGuid();
                logger.LogError(ex,$"{errorID}: {ex.Message}");
                
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorID,
                    ErrorMessage = "Something went wrong!"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }

    }
}
