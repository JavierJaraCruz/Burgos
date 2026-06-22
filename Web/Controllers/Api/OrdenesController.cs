using Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/ordenes")]
    public class OrdenesController : ApiController
    {
        private readonly OrdenService ordenService = new OrdenService();
        private readonly CarritoService carritoService = new CarritoService();
        private readonly ProductoService productoService = new ProductoService();

        [HttpPost]
        [Route("comprar/{usuarioId}")]
        public IHttpActionResult Comprar(int usuarioId)
        {
            try
            {
                var carrito = carritoService.ObtenerPorUsuario(usuarioId);

                if (carrito == null)
                    return BadRequest("No existe carrito");

                var detallesCarrito =
                    carritoService.ObtenerDetalles(carrito.CarritoId);

                if (detallesCarrito.Count == 0)
                    return BadRequest("Carrito vacío");

                List<OrdenDetalle> detallesOrden =
                    new List<OrdenDetalle>();

                foreach (var item in detallesCarrito)
                {
                    detallesOrden.Add(new OrdenDetalle
                    {
                        ProductoId = item.ProductoId,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario,
                        Subtotal = item.Subtotal
                    });

                    productoService.ActualizarStock(
                        item.ProductoId,
                        item.Cantidad,
                        "SALIDA",
                        "COMPRA"
                    );
                }

                int ordenId =
                    ordenService.CrearOrden(
                        usuarioId,
                        detallesOrden
                    );

                carritoService.VaciarCarrito(
                    carrito.CarritoId
                );

                carritoService.EliminarCarrito(
                    carrito.CarritoId
                );

                return Ok(new
                {
                    OrdenId = ordenId,
                    Mensaje = "Compra realizada"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Error = ex.Message,
                    Inner = ex.InnerException?.Message
                });
            }
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Listar()
        {
            return Ok(ordenService.ListarOrdenes());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Obtener(int id)
        {
            var orden = ordenService.ObtenerPorId(id);

            if (orden == null)
                return NotFound();

            return Ok(orden);
        }
    }
}