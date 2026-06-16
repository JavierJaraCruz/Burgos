using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;


namespace Services
{
    public class CarritoService
    {
        private readonly CarritoDAL carritoDAL = new CarritoDAL();

        public Carrito ObtenerPorUsuario(int usuarioId)
            => carritoDAL.ObtenerPorUsuario(usuarioId);

        public Carrito ObtenerCarrito(int Id)
            => carritoDAL.ObtenerCarrito(Id);


        public int CrearCarrito(int usuarioId) => carritoDAL.CrearCarrito(usuarioId);

        public void AgregarProducto(int carritoId, int productoId, int cantidad, decimal precioUnitario)
            => carritoDAL.AgregarProducto(carritoId, productoId, cantidad, precioUnitario);

        public void EliminarProducto(int detalleId)
            => carritoDAL.EliminarProducto(detalleId);

        public void EliminarCarrito(int id) => carritoDAL.Eliminar(id);
    }
}