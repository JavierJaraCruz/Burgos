using Entities;


namespace Web.ViewModels
{
    public class DashboardViewModel
    {
        public int CantidadProductos { get; set; }
        public int CantidadVentas { get; set; }
        public int StockBajo { get; set; }
        public decimal TotalVentasMonto { get; set; }
        public string MensajeEstado { get; set; }

        // opcional: si quieres traer el entity completo
        public Dashboard Datos { get; set; }
    }
}