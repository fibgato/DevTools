using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevTools.WebAPI.Core.User;

namespace NetDevTools.Web.MVC.Controllers
{
    public class AuthController : MainController
    {
        private readonly IDevToolsUser _devToolsUser;

        public AuthController(IDevToolsUser devToolsUser)
        {
            _devToolsUser = devToolsUser;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("novo-usuario")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(string returnUrl = null)
        {
            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {

            return RedirectToAction("Index", "Login");
        }
    }
}
