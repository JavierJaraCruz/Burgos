using Entities;
using Services;
using System.Collections.Generic;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        private readonly ProductoService productoService = new ProductoService();

        // GET api/productos
        [HttpGet]
        [Route("")]
        public IHttpActionResult ListarProductos()
        {
            List<Producto> productos = productoService.ListarProductos();
            return Ok(productos);
        }

        // GET api/productos/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult ObtenerProducto(int id)
        {
            Producto producto = productoService.ObtenerProducto(id);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }
    }
}