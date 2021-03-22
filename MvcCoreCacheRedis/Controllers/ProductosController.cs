using Microsoft.AspNetCore.Mvc;
using MvcCoreCacheRedis.Models;
using MvcCoreCacheRedis.Repositories;
using MvcCoreCacheRedis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCacheRedis.Controllers
{
    public class ProductosController : Controller
    {
        RepositoryProductos repo;
        ServiceCacheRedis serviceCache;
        public ProductosController (RepositoryProductos repo, ServiceCacheRedis serviceCache)
        {
            this.repo = repo;
            this.serviceCache = serviceCache;
        }
        public IActionResult Index()
        {
            return View(this.repo.GetProductos());
        }
        public IActionResult Details(string id)
        {
            return View(this.repo.BuscarProducto(id));
        }

        public IActionResult SeleccionarFavorito(string id)
        {
            Producto producto = this.repo.BuscarProducto(id);
            this.serviceCache.AlmacenarProducto(producto);
            return RedirectToAction("Index");
        }
        public IActionResult Favoritos()
        {
            return View(this.serviceCache.GetProductos());
        }
        public IActionResult EliminarFavorito(string id)
        {
            this.serviceCache.EliminarProducto(id);
            return RedirectToAction("Favoritos");
        }
    }
}
