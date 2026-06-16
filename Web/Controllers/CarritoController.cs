using Services;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Web.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        private readonly CarritoService carritoService = new CarritoService();

        public ActionResult Crear(int usuarioId)
        {
            int id = carritoService.CrearCarrito(usuarioId);
            return RedirectToAction("Detalle", new { id });
        }

        public Carrito ObtenerPorUsuario(int usuarioId) => carritoService.ObtenerPorUsuario((int)usuarioId);
        public Carrito ObtenerCarrito(int id) => carritoService.ObtenerCarrito((int)id);

        public ActionResult Detalle(int id) => View(carritoService.ObtenerCarrito(id));

        public ActionResult Eliminar(int id)
        {
            carritoService.EliminarCarrito(id);
            return RedirectToAction("Index", "Producto");
        }
    }
}