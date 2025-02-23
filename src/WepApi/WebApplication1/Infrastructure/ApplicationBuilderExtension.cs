using HotelVR.Common.Infrastructure.Exceptions;
using HotelVR.Common.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace HotelApi.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app,
        bool includeExceptionsDetails = false,
        bool useDefaultHandlingResponse = true,
        Func<HttpContext, Exception, Task> HandleException = null)
        {

            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                    if (!useDefaultHandlingResponse && HandleException == null)
                    {
                        throw new ArgumentNullException(nameof(HandleException),
                            $"{nameof(HandleException)} cannot be null when {nameof(useDefaultHandlingResponse)} is false");
                    }

                    if (!useDefaultHandlingResponse && HandleException != null)
                    {
                        await HandleException(context, exceptionObject?.Error);
                        return;
                    }

                    await DefaultHandleException(context, exceptionObject.Error, includeExceptionsDetails);
                    return;

                });
            });


            return app;
        }

        private static async Task DefaultHandleException(HttpContext context, Exception exception, bool includeExceptionsDetails)
        {

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Internal server error occured";
            if (exception is UnauthorizedAccessException)
                statusCode = HttpStatusCode.Unauthorized;

            if (exception is DataBaseValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                var validationResponse = new ValidationResponseModel(exception.Message);
                await WriteResponse(context, statusCode, validationResponse);
                return;
            }

            var res = new
            {
                HttpStatusCode = (int)statusCode,
                detail = includeExceptionsDetails ? exception.ToString() : message,
            };

            await WriteResponse(context, statusCode, res);
        }

        private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, object responseObj)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(responseObj);
        }
    }
}
