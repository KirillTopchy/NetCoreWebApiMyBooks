using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using MyBooks.Data.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MyBooks.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            var contextRequest = httpContext.Features.Get<IHttpRequestFeature>();

            var response = new ErrorVM()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal Server Error from the custom Middlewate",
                Path = contextRequest.Path
            };

            return httpContext.Response.WriteAsync(response.ToString());
        }
    }
}
