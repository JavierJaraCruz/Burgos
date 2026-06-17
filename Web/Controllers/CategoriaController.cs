using Entities;
using Services;
using System.Linq;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [ValidarSesion]
    public class CategoriaController : Controller
    {
        private readonly CategoriaService categoriaService = new CategoriaService();


    public ActionResult Index()
        {
            var categorias = categoriaService.ListarCategorias();

            var lista = categorias.Select(c => new CategoriaViewModel
            {
                CategoriaId = c.CategoriaId,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            }).ToList();

            return View(lista);
        }

        public ActionResult Detalle(int id)
        {
            var categoria = categoriaService.ObtenerCategoria(id);

            if (categoria == null)
                return HttpNotFound();

            var vm = new CategoriaViewModel
            {
                CategoriaId = categoria.CategoriaId,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };

            return View(vm);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View(new CategoriaViewModel());
        }

        [HttpPost]
        public ActionResult Crear(CategoriaViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var categoria = new Categoria
            {
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion
            };

            categoriaService.CrearCategoria(categoria);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var categoria = categoriaService.ObtenerCategoria(id);

            if (categoria == null)
                return HttpNotFound();

            var vm = new CategoriaViewModel
            {
                CategoriaId = categoria.CategoriaId,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Editar(CategoriaViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var categoria = new Categoria
            {
                CategoriaId = vm.CategoriaId,
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion
            };

            categoriaService.ActualizarCategoria(categoria);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var categoria = categoriaService.ObtenerCategoria(id);

            if (categoria == null)
                return HttpNotFound();

            var vm = new CategoriaViewModel
            {
                CategoriaId = categoria.CategoriaId,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult EliminarConfirmado(int CategoriaId)
        {
            categoriaService.EliminarCategoria(CategoriaId);

            return RedirectToAction("Index");
        }
    }


}
