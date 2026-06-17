using Services;
using System.Web.Mvc;

namespace Web.Controllers
{
    [ValidarSesion]
    public class InventarioController : Controller
    {
        private readonly InventarioService inventarioService = new InventarioService();

        public ActionResult Movimientos(int productoId)
            => View(inventarioService.ListarMovimientos(productoId));
    }


}