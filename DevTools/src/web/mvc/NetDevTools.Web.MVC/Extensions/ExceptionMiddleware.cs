using NetDevTools.Core.Exceptions;
using Polly.CircuitBreaker;
using System.Net;

namespace NetDevTools.Web.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        //private static IAutenticacaoService _autenticacaoService;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //_autenticacaoService = autenticacaoService;

            try
            {
                await _next(httpContext);
            }
            //catch (CustomHttpRequestException ex)
            //{
            //    await HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            //}
            //catch (CustomHttpRedirectException ex)
            //{
            //    HandlerRedirectValidationException(httpContext, ex);
            //}
            catch (CustomAuthorizeException)
            {
                await HandleAuthorizationValidationException(httpContext);
            }
            catch (BrokenCircuitException)
            {
                HandleCircuitBreakerExceptionAsync(httpContext);
            }

        }

        private static async Task HandleAuthorizationValidationException(HttpContext httpContext)
        {
            //var sucesso = await UtilizarRefreshToken(httpContext);

            //if (sucesso) return;

            await HandleRequestExceptionAsync(httpContext, HttpStatusCode.Unauthorized);
        }

        private static async Task HandleRequestExceptionAsync(HttpContext httpContext, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                //var sucesso = await UtilizarRefreshToken(httpContext);

                //if (sucesso) return;

                //httpContext.Response.Redirect("/sem-confirmacao");

                return;
            }

            httpContext.Response.StatusCode = (int)statusCode;
        }

        private static void HandleCircuitBreakerExceptionAsync(HttpContext context)
        {
            context.Response.Redirect("/sistema-indisponivel");
        }

        //private static async Task<bool> UtilizarRefreshToken(HttpContext httpContext)
        //{
        //    await _autenticacaoService.Logout();

        //    if (_autenticacaoService.RefreshTokenValido().Result)
        //    {
        //        httpContext.Response.Redirect(httpContext.Request.Path);
        //        return true;
        //    }

        //    return false;
        //}
    }
}
