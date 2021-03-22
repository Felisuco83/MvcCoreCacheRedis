using MvcCoreCacheRedis.Helpers;
using MvcCoreCacheRedis.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCacheRedis.Services
{
    public class ServiceCacheRedis
    {
        private IDatabase cacheredis;

        public ServiceCacheRedis()
        {
            this.cacheredis = CacheRedisMultiplexer.Connection.GetDatabase();
        }

        public List<Producto> GetProductos() 
        {
            string jsonProductos = this.cacheredis.StringGet("favoritos");
            if(jsonProductos != null)
            {
                List<Producto> productos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
                return productos;
            }else
            {
                return null;
            }
        }

        public void AlmacenarProducto (Producto producto)
        {
            string jsonProductos = this.cacheredis.StringGet("favoritos");
            List<Producto> productos;
            if(jsonProductos == null)
            {
                productos = new List<Producto>();
            }else
            {
                productos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
            }
            productos.Add(producto);
            jsonProductos = JsonConvert.SerializeObject(productos);
            this.cacheredis.StringSet("favoritos", jsonProductos, TimeSpan.FromMinutes(15));
        }

        public void EliminarProducto(string id)
        {
            string jsonProductos = this.cacheredis.StringGet("favoritos");
            if (jsonProductos != null)
            {
                List<Producto> productos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
                Producto prod = productos.SingleOrDefault(x => x.IdProducto == id);
                productos.Remove(prod);
                if(productos.Count == 0)
                {
                    this.cacheredis.KeyDelete("favoritos");
                } else
                {
                    jsonProductos = JsonConvert.SerializeObject(productos);
                    this.cacheredis.StringSet("favoritos", jsonProductos, TimeSpan.FromMinutes(15));
                }
            }
        }
    }
}
