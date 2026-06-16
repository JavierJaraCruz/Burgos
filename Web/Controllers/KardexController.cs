using Services;
using System.Linq;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class KardexController : Controller
    {
        private readonly KardexService kardexService =
        new KardexService();


    public ActionResult Index(int productoId)
        {
            var kardex = kardexService
                .ObtenerKardex(productoId);

            var lista = kardex.Select(k => new KardexViewModel
            {
                ProductoId = k.ProductoId,
                Fecha = k.Fecha,
                TipoMovimiento = k.TipoMovimiento,
                Cantidad = k.Cantidad,
                Referencia = k.Referencia,
                Saldo = k.Saldo
            }).ToList();

            ViewBag.ProductoId = productoId;

            return View(lista);
        }
    }


}
