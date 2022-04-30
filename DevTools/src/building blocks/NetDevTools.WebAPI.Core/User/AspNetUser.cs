using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NetDevTools.WebAPI.Core.User
{
    public class DevToolsUser : IDevToolsUser
    {
        private readonly IHttpContextAccessor _accessor;

        public DevToolsUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid ObterUserId()
        {
            return EstaAutenticado() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public bool EstaAutenticado()
        {
            try
            {
                return _accessor.HttpContext.User.Identity.IsAuthenticated;
            }
            catch
            {
                return false;
            }
        }

        public string ObterUserToken()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.GetUserToken() : string.Empty;
        }

        public string ObterUserEmail()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.GetUserEmail() : string.Empty;
        }

        public string ObterUserSaudacao()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.GetUserSaudacao() : string.Empty;
        }

        public bool PossuiRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public HttpContext ObterHttpContext()
        {
            return _accessor.HttpContext;
        }

        public string ObterUserRefreshToken()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.GetUserRefreshToken() : string.Empty;
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}
