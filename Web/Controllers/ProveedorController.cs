using Entities;
using Services;
using System.Linq;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly ProveedorService proveedorService =
        new ProveedorService();


    public ActionResult Index()
        {
            var proveedores = proveedorService.ListarProveedores();

            var lista = proveedores.Select(p => new ProveedorViewModel
            {
                ProveedorId = p.ProveedorId,
                Nombre = p.Nombre,
                Email = p.Email,
                Telefono = p.Telefono,
                Direccion = p.Direccion
            }).ToList();

            return View(lista);
        }

        public ActionResult Detalle(int id)
        {
            var proveedor = proveedorService.ObtenerProveedor(id);

            if (proveedor == null)
                return HttpNotFound();

            var vm = new ProveedorViewModel
            {
                ProveedorId = proveedor.ProveedorId,
                Nombre = proveedor.Nombre,
                Email = proveedor.Email,
                Telefono = proveedor.Telefono,
                Direccion = proveedor.Direccion
            };

            return View(vm);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View(new ProveedorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(ProveedorViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var proveedor = new Proveedor
            {
                Nombre = vm.Nombre,
                Email = vm.Email,
                Telefono = vm.Telefono,
                Direccion = vm.Direccion
            };

            proveedorService.CrearProveedor(proveedor);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var proveedor = proveedorService.ObtenerProveedor(id);

            if (proveedor == null)
                return HttpNotFound();

            var vm = new ProveedorViewModel
            {
                ProveedorId = proveedor.ProveedorId,
                Nombre = proveedor.Nombre,
                Email = proveedor.Email,
                Telefono = proveedor.Telefono,
                Direccion = proveedor.Direccion
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ProveedorViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var proveedor = new Proveedor
            {
                ProveedorId = vm.ProveedorId,
                Nombre = vm.Nombre,
                Email = vm.Email,
                Telefono = vm.Telefono,
                Direccion = vm.Direccion
            };

            proveedorService.ActualizarProveedor(proveedor);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var proveedor = proveedorService.ObtenerProveedor(id);

            if (proveedor == null)
                return HttpNotFound();

            var vm = new ProveedorViewModel
            {
                ProveedorId = proveedor.ProveedorId,
                Nombre = proveedor.Nombre,
                Email = proveedor.Email,
                Telefono = proveedor.Telefono,
                Direccion = proveedor.Direccion
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult EliminarConfirmado(int ProveedorId)
        {
            proveedorService.EliminarProveedor(ProveedorId);

            return RedirectToAction("Index");
        }
    }


}
