using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using System.Collections;
using System.Security;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            IEnumerable<ValidationFailure> errors;

            if (e.GetType() == typeof(ValidationException))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                errors = ((ValidationException)e).Errors;

                var errorPropertyDetail = new List<ErrorPropertyDetails>();

                foreach (var error in errors)
                {
                    var err = new ErrorPropertyDetails
                    {
                        PropertyName = error.PropertyName,
                        ErrorMessage = error.ErrorMessage,
                    };
                    errorPropertyDetail.Add(err);
                }

                return httpContext.Response.WriteAsync(new ValidationErrorDetail
                {
                    Status = false,
                    Message = "Validation Error",
                    Errors = errorPropertyDetail
                }.ToString());
            }

            if (e.GetType() == typeof(SecurityException))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                return httpContext.Response.WriteAsync(new ErrorDetails
                {
                    Status = false,
                    Message = "Authentication Error",
                }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                Status = false,
                Message = "Internal Server Error"
            }.ToString());
        }
    }
}
