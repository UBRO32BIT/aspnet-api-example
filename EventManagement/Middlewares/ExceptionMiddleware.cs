﻿using EventManagement.Models;
using Microsoft.Identity.Client;
using System.Net;

namespace EventManagement.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandlerExceptionAsync(context, ex);
            }
        }

        private async Task HandlerExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problem = new();

            switch (ex)
            {
                //case BadRequestException badRequestException:
                //    statusCode = HttpStatusCode.BadRequest;
                //    problem = new CustomProblemDetails
                //    {
                //        Title = badRequestException.Message,
                //        Status = (int)statusCode,
                //        Detail = badRequestException.InnerException?.Message,
                //        Type = nameof(BadRequestException),
                //        Errors = badRequestException.ValidationErrors
                //    };

                //    break;
                //case NotFoundException notFound:
                //    statusCode = HttpStatusCode.NotFound;
                //    problem = new CustomProblemDetails
                //    {
                //        Title = notFound.Message,
                //        Status = (int)statusCode,
                //        Type = nameof(NotFoundException),
                //        Detail = notFound.InnerException?.Message
                //    };
                //    break;
                //case ForbiddenAccessException forbidden:
                //    statusCode = HttpStatusCode.Forbidden;
                //    problem = new CustomProblemDetails
                //    {
                //        Title = forbidden.Message,
                //        Status = (int)statusCode,
                //        Detail = forbidden.InnerException?.Message,
                //        Type = nameof(ForbiddenAccessException)
                //    };
                //    break;
                default:
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
