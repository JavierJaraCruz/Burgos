using Entities;
using Services;
using System.Web.Http;


namespace API.Controllers
{
    [RoutePrefix("api/carrito")]
    public class CarritoController : ApiController
    {
        private readonly CarritoService service = new CarritoService();
        private readonly ProductoService productoService = new ProductoService();

        [HttpPost]
        [Route("agregar")]
        public IHttpActionResult Agregar(CarritoRequest request)
        {
            if (request == null)
                return BadRequest("Request inválido");

            
            var carrito = service.ObtenerPorUsuario(request.UsuarioId);

            int carritoId;

            if (carrito == null)
                carritoId = service.CrearCarrito(request.UsuarioId);
            else
                carritoId = carrito.CarritoId;

            
            var producto = productoService.ObtenerProducto(request.ProductoId);

            if (producto == null)
                return NotFound();

         
            service.AgregarProducto(
                carritoId,
                request.ProductoId,
                request.Cantidad,
                producto.Precio
            );

            return Ok(new { mensaje = "Agregado al carrito" });
        }
        [HttpGet]
        [Route("usuario/{usuarioId}")]
        public IHttpActionResult ObtenerCarrito(int usuarioId)
        {
            var carrito = service.ObtenerPorUsuario(usuarioId);

            if (carrito == null)
                return NotFound();

            var detalles = service.ObtenerDetalles(carrito.CarritoId);

            return Ok(new
            {
                CarritoId = carrito.CarritoId,
                Productos = detalles
            });
        }
    }
}