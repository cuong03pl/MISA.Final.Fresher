using Microsoft.AspNetCore.Http;
using MISA.QLTS.Core.DTOs.Response;
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
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(ServiceResponse<object>.Error(ex.Message, ex.Message), options);
                await context.Response.WriteAsync(json);
            }
            catch (ValidateException ex)
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(ServiceResponse<object>.Error(ex.Message, ex.Message), options);
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
