using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NetDevTools.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace NetDevTools.WebAPI.Core.CustomResponses
{
    public static class CustomExceptionResult
    {
        public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.Use(WriteDevelopmentResponse);
            }
            else
            {
                app.Use(WriteProductionResponse);
            }

        }

        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, true);

        private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, false);

        private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            var exception = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exception?.Error;

            if (ex != null)
            {
                var erros = new List<string>();

                erros.Add(ex.Message);

                if (includeDetails) erros.Add(ex.ToString());

                var retError = new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "Mensagens", erros.Select(s => s).ToArray() }
                });

                retError.Title = "Ocorreu um erro ao processar a requisição";

                httpContext.Response.ContentType = "application/json";

                switch (ex)
                {
                    case CustomBadRequestException _:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        retError.Status = (int)HttpStatusCode.BadRequest;
                        break;
                    case CustomNotFoundException _:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        retError.Status = (int)HttpStatusCode.BadRequest;
                        break;
                    case CustomAuthorizeException _:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        retError.Status = (int)HttpStatusCode.Unauthorized;
                        break;
                    case CustomForbiddenException _:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        retError.Status = (int)HttpStatusCode.Forbidden;
                        break;
                    default:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        retError.Status = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var stream = httpContext.Response.Body;

                await JsonSerializer.SerializeAsync(stream, retError);
            }
        }
    }
}
