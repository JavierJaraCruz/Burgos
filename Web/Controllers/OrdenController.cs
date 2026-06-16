using Entities;
using Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class OrdenController : Controller
    {
        private readonly OrdenService ordenService = new OrdenService();

        public ActionResult Index() => View(ordenService.ListarOrdenes());

        [HttpGet]
        public ActionResult Crear() => View();

        [HttpPost]
        public ActionResult Crear(int usuarioId, List<OrdenDetalle> detalles)
        {
            if (ModelState.IsValid)
            {
                ordenService.CrearOrden(usuarioId, detalles);
                return RedirectToAction("Index");
            }
            return View(detalles);
        }
    }
}