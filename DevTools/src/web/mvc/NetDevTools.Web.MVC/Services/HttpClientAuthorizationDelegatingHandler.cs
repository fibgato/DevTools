using NetDevTools.WebAPI.Core.User;
using System.Net.Http.Headers;

namespace NetDevTools.Web.MVC.Services
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IDevToolsUser _aspNetUser;

        public HttpClientAuthorizationDelegatingHandler(IDevToolsUser aspNetUser)
        {
            _aspNetUser = aspNetUser;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _aspNetUser.ObterHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }

            var token = _aspNetUser.ObterUserToken();

            if (token != null) request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
