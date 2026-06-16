using Entities;
using Services;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class RolController : Controller
    {
        private readonly RolService rolService = new RolService();

        public ActionResult Index() => View(rolService.ListarRoles());

        [HttpGet]
        public ActionResult Crear() => View();

        [HttpPost]
        public ActionResult Crear(Rol r)
        {
            if (ModelState.IsValid)
            {
                rolService.CrearRol(r);
                return RedirectToAction("Index");
            }
            return View(r);
        }

        public ActionResult Eliminar(int id)
        {
            rolService.EliminarRol(id);
            return RedirectToAction("Index");
        }
    }
}