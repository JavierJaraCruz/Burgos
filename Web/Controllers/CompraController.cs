using Entities;
using Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class CompraController : Controller
    {
        private readonly CompraService compraService = new CompraService();

        public ActionResult Index() => View(compraService.ListarCompras());

        [HttpGet]
        public ActionResult Crear() => View();

        [HttpPost]
        public ActionResult Crear(int proveedorId, List<CompraDetalle> detalles)
        {
            if (ModelState.IsValid)
            {
                compraService.CrearCompra(proveedorId, detalles);
                return RedirectToAction("Index");
            }
            return View(detalles);
        }
    }
}