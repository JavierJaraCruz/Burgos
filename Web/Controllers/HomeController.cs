using System.Web.Mvc;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    [ValidarSesion]
    public class HomeController : Controller
    {
        private readonly DashboardService _dashboardService;

        public HomeController()
        {
            _dashboardService = new DashboardService();
        }

        public ActionResult Index()
        {
            var data = _dashboardService.ObtenerDashboard();

            var vm = new DashboardViewModel
            {
                CantidadProductos = data.CantidadProductos,
                CantidadVentas = data.CantidadVentas,
                StockBajo = data.StockBajo,
                TotalVentasMonto = data.TotalVentasMonto,
                MensajeEstado = _dashboardService.ObtenerMensajeEstado(data)
            };

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}