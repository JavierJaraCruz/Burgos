using Entities;
using Services;
using System.Linq;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [ValidarSesion]
    public class ProductoController : Controller
    {
        private readonly ProductoService productoService = new ProductoService();
        private readonly CategoriaService categoriaService = new CategoriaService();


    public ActionResult Index()
        {
            var productos = productoService.ListarProductos();

            var lista = productos.Select(p => new ProductoViewModel
            {
                ProductoId = p.ProductoId,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId,
                CategoriaNombre = p.CategoriaNombre,
                ImagenUrl = p.ImagenUrl,
                Activo = p.Activo
            }).ToList();

            return View(lista);
        }

        public ActionResult Detalle(int id)
        {
            var producto = productoService.ObtenerProducto(id);

            if (producto == null)
                return HttpNotFound();

            var vm = new ProductoViewModel
            {
                ProductoId = producto.ProductoId,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = producto.CategoriaNombre,
                ImagenUrl = producto.ImagenUrl,
                Activo = producto.Activo
            };

            return View(vm);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            var vm = new ProductoViewModel();

            vm.Categorias = categoriaService.ListarCategorias()
                .Select(c => new SelectListItem
                {
                    Value = c.CategoriaId.ToString(),
                    Text = c.Nombre
                });

            return View(vm);
        }

        [HttpPost]
        public ActionResult Crear(ProductoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categorias = categoriaService.ListarCategorias()
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoriaId.ToString(),
                        Text = c.Nombre
                    });

                return View(vm);
            }

            var producto = new Producto
            {
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion,
                Precio = vm.Precio,
                Stock = vm.Stock,
                CategoriaId = vm.CategoriaId,
                ImagenUrl = vm.ImagenUrl,
                Activo = true
            };

            productoService.CrearProducto(producto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var producto = productoService.ObtenerProducto(id);

            if (producto == null)
                return HttpNotFound();

            var vm = new ProductoViewModel
            {
                ProductoId = producto.ProductoId,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = producto.CategoriaNombre,
                ImagenUrl = producto.ImagenUrl,
                Activo = producto.Activo
            };

            vm.Categorias = categoriaService.ListarCategorias()
                .Select(c => new SelectListItem
                {
                    Value = c.CategoriaId.ToString(),
                    Text = c.Nombre
                });

            return View(vm);
        }

        [HttpPost]
        public ActionResult Editar(ProductoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categorias = categoriaService.ListarCategorias()
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoriaId.ToString(),
                        Text = c.Nombre
                    });

                return View(vm);
            }

            var producto = new Producto
            {
                ProductoId = vm.ProductoId,
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion,
                Precio = vm.Precio,
                Stock = vm.Stock,
                CategoriaId = vm.CategoriaId,
                ImagenUrl = vm.ImagenUrl,
                Activo = vm.Activo
            };

            productoService.ActualizarProducto(producto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var producto = productoService.ObtenerProducto(id);

            if (producto == null)
                return HttpNotFound();

            var vm = new ProductoViewModel
            {
                ProductoId = producto.ProductoId,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = producto.CategoriaNombre,
                ImagenUrl = producto.ImagenUrl,
                Activo = producto.Activo
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult EliminarConfirmado(int ProductoId)
        {
            productoService.EliminarProducto(ProductoId);

            return RedirectToAction("Index");
        }
    }


}
