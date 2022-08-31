using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace NetDevTools.Core.Data.Redis
{
    public interface IRedisCache
    {
        Task<T> ObterPorId<T>(string key) where T : class;
        Task Adicionar<T>(string key, T obj) where T : class;
    }

    public class RedisCache : IRedisCache
    {
        private readonly IDistributedCache _redisCache;

        public RedisCache(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<T> ObterPorId<T>(string key) where T : class
        {
            var res = await _redisCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(res)) return null;

            return DeserializarObjetoResponse<T>(res);
        }

        public async Task Adicionar<T>(string key, T obj) where T : class
        {
            await _redisCache.SetStringAsync(key, ObterConteudo(obj), ObterConfiguracaoEntrada());
        }

        private DistributedCacheEntryOptions ObterConfiguracaoEntrada()
        {
            return new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.UtcNow.AddMinutes(60),
                SlidingExpiration = TimeSpan.FromMinutes(15)
            };
        }

        private string ObterConteudo(object dado)
        {
            return JsonSerializer.Serialize(dado);
        }

        private T DeserializarObjetoResponse<T>(string dado)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(dado, options);
        }
    }
}
