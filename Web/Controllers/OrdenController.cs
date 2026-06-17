using Services;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class OrdenController : Controller
    {
        private readonly OrdenService ordenService = new OrdenService();

        // GET: Orden
        public ActionResult Index()
        {
            var ordenes = ordenService.ListarOrdenes();

            var lista = ordenes.Select(o => new OrdenViewModel
            {
                OrdenId = o.OrdenId,
                UsuarioId = o.UsuarioId,
                FechaOrden = o.FechaOrden,
                Total = o.Total,
                Estado = o.Estado
            }).ToList();

            return View(lista);
        }

        // GET: Orden/Crear
        public ActionResult Crear()
        {
            var model = new OrdenViewModel();

            var usuarioService = new UsuarioService();

            model.Usuarios = usuarioService.ListarUsuarios()
                .Select(u => new SelectListItem
                {
                    Value = u.UsuarioId.ToString(),
                    Text = u.NombreUsuario
                })
                .ToList();

            model.Detalles.Add(new OrdenDetalleViewModel());

            return View(model);
        }

        // POST: Orden/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(OrdenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            List<OrdenDetalle> detalles = model.Detalles.Select(d => new OrdenDetalle
            {
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            }).ToList();

            ordenService.CrearOrden(model.UsuarioId, detalles);

            return RedirectToAction("Index");
        }
        public ActionResult Detalle(int id)
        {
            var orden = ordenService.ObtenerPorId(id);

            if (orden == null)
                return HttpNotFound();

            var model = new OrdenViewModel
            {
                OrdenId = orden.OrdenId,
                UsuarioId = orden.UsuarioId,
                FechaOrden = orden.FechaOrden,
                Total = orden.Total,
                Estado = orden.Estado
            };

            return View(model);
        }
    }
}