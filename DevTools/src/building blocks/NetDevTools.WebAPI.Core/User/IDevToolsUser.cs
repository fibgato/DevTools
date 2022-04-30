using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NetDevTools.WebAPI.Core.User
{
    public interface IDevToolsUser
    {
        string Name { get; }
        Guid ObterUserId();
        string ObterUserEmail();
        string ObterUserSaudacao();
        string ObterUserToken();
        string ObterUserRefreshToken();
        bool EstaAutenticado();
        bool PossuiRole(string role);
        IEnumerable<Claim> ObterClaims();
        HttpContext ObterHttpContext();
    }
}
