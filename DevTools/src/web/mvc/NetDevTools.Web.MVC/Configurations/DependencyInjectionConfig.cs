using NetDevTools.Web.MVC.Services;
using NetDevTools.WebAPI.Core.User;

namespace NetDevTools.Web.MVC.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddMemoryCache();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDevToolsUser, DevToolsUser>();
        }
    }
}
