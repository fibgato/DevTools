using Microsoft.Extensions.DependencyInjection;

namespace NetDevTools.Core.Data.Redis
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, string connectionString, string instanceName)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = connectionString;
                options.InstanceName = instanceName;
            });

            services.AddSingleton<IRedisCache, RedisCache>();

            return services;
        }
    }
}
