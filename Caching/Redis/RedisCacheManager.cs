using forgeSampleAPI_DotNetCore.Core.CrossCuttingCornces.Caching;
using Newtonsoft.Json;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forgeSampleAPI_DotNetCore.Caching.Redis
{
    public class RedisCacheManager:ICacheManager
    {
        private RedisServer _redisServer;

        public RedisCacheManager(RedisServer redisServer)
        {
            this._redisServer = redisServer;
        }
        public void Add(string key, object data, int duration)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            _redisServer.Database.StringSet(key, jsonData, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
          
            string jsonData = _redisServer.Database.StringGet(key);
            return JsonConvert.DeserializeObject<T>(jsonData);
           
        }

        public object Get(string key)
        {   
             string jsonData = _redisServer.Database.StringGet(key);
             var result=JsonConvert.DeserializeObject<object>(jsonData);
             return result;

            
        }

        public object Get(string key, Type type)
        {
            
            string jsonData = _redisServer.Database.StringGet(key);
            var result=JsonConvert.DeserializeObject(jsonData, type);
            return result;

            
          
        }

        public bool IsAdd(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public void Remove(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }

        public void RemoveByPattern(string key)
        {
            throw new NotImplementedException();
        }
    }
}
