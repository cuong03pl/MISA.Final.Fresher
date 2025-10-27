using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.Core.Exceptions
{
    public class ValidateExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ValidateExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidateException ex)
            {
                context.Response.StatusCode = 400;
                var errorResponse = new
                {
                    devMsg = ex.Message,
                    Message = ex.Message
                };
                context.Response.ContentType = "application/json";

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(ex.Message);
            }

        }
    }
}
