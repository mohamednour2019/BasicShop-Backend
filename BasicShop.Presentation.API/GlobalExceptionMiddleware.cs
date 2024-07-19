using Azure;
using BasicShop.Core.Domain.Entities;
using BasicShop.Shared.CustomExceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BasicShop.Presentation.API
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                string message=ex.Message;
                httpContext.Response.ContentType = "application/json";
                if (ex is ViolenceConstraintException || ex is NotFoundException)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    message = ex.Message;
                }
                else if (ex is SecurityTokenExpiredException)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                else
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    message = "Sorry, Something Went Wrong Please Try Again Later.";
                }
                var response = new ResponseModel<object>(null, message, false);
                string responseBody = JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsync(responseBody);
            }


        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
