namespace API.Controllers
{
    public class CarritoRequest
    {
        public int UsuarioId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}