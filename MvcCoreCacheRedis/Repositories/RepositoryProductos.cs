using MvcCoreCacheRedis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCoreCacheRedis.Repositories
{
    public class RepositoryProductos
    {
        PathProvider pathProvider;
        XDocument docxml;

        public RepositoryProductos(PathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
            string path = this.pathProvider.MapPath("productos.xml", Folders.Documents);
            this.docxml = XDocument.Load(path);
        }

        public List<Producto> GetProductos()
        {
            var consulta = from datos in this.docxml.Descendants("producto")
                           select new Producto
                           {
                               IdProducto = datos.Element("idproducto").Value,
                               Nombre = datos.Element("nombre").Value,
                               Descripcion = datos.Element("descripcion").Value,
                               Precio = datos.Element("precio").Value,
                               Imagen = datos.Element("imagen").Value,
                           };
            return consulta.ToList();
        }

        public Producto BuscarProducto (string id)
        {
            return this.GetProductos().SingleOrDefault(x => x.IdProducto == id);
        }
    }
}
