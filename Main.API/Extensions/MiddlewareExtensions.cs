using System.Net;
using Main.API.DtoModels;
using Microsoft.AspNetCore.Diagnostics;

namespace Main.API.Extensions;

public static class MiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
    {
        app.UseExceptionHandler(appError =>
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextExceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextExceptionFeature != null)
                {
                    var error = contextExceptionFeature.Error;
                    logger.LogError($"Something went wrong {error}, on the route" +
                                    $"{contextExceptionFeature.Path}");

                    await context.Response.WriteAsync(new GlobalError
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = error.Message
                    }.ToString());
                }
            }));
    }
}