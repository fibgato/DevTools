using Microsoft.AspNetCore.Authentication.Cookies;

namespace NetDevTools.Web.MVC.Configurations
{
    public static class IdentityConfig
    {
        public static void AddIdentityConfigurations(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/acesso-negado";
                });
        }

        public static void UseIdentityConfigurations(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
