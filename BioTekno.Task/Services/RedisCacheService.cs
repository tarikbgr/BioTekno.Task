using BioTekno.Task.Models.Entities;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;

namespace BioTekno.Task.Services
{
    public interface IRedisCacheService<T> 
    {
        Task<T> GetCache(string key);

        Task<bool> SetCache(string key, T t);
    }

    public class RedisCacheService<T> : IRedisCacheService<T>
    {
        private readonly IConnectionMultiplexer _redisCon;
        private readonly IDatabase _cache;
       
        private TimeSpan ExpireTime => TimeSpan.FromMinutes(20);
        public RedisCacheService(IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase();
        }

        public async Task<T> GetCache(string key)
        {
            var response = await _cache.StringGetAsync(key);
            if (response.IsNull)
            {
                return default;
            }

            var valueObject = JsonConvert.DeserializeObject<T>(response);
            
            return valueObject;
        }

        public async Task<bool> SetCache(string key, T t)
        {
            var response = await _cache.StringSetAsync(key, JsonConvert.SerializeObject(t), ExpireTime);
            return response;
        }
    }

}
