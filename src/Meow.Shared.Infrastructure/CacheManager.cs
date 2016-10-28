using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meow.Shared.Infrastructure
{
    public class CacheManager
    {
        private ConnectionMultiplexer _redis;

        public static CacheManager Create()
        {
            return new CacheManager();
        }

        public CacheManager()
        {
            _redis = ConnectionMultiplexer.Connect("localhost");
        }

        public void Set(string key, string value)
        {
            var db = _redis.GetDatabase();
            db.StringSet(key, value);
        }

        public string Get(string key)
        {
            var db = _redis.GetDatabase();
            return db.StringGet(key);
        }

        public void Del(string key)
        {
            var db = _redis.GetDatabase();
            db.KeyDelete(key);
        }
    }
}
