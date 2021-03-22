using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCacheRedis.Helpers
{
    public static class CacheRedisMultiplexer
    {
        private static Lazy<ConnectionMultiplexer> CrearConexion = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("cacheazuretete.redis.cache.windows.net:6380,password=Po+TSyvC2KcRj8dA11QVw15g7Iyy5eAZ8l2oAmX52JE=,ssl=True,abortConnect=False");
        });

        public static ConnectionMultiplexer Connection
        {
            get { return CrearConexion.Value; }
        }
    }
}
