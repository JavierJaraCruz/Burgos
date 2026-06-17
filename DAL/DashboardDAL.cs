using Entities;
using System;
using System.Configuration;
using System.Data.SqlClient;


namespace Web.DAL
{
    public class DashboardDAL
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["burgos"].ConnectionString;

        public Dashboard ObtenerMetricas()
        {
            var model = new Dashboard();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                model.CantidadProductos = (int)new SqlCommand(
                    "SELECT COUNT(*) FROM Productos WHERE Activo = 1", conn
                ).ExecuteScalar();

                model.CantidadVentas = (int)new SqlCommand(
                    "SELECT COUNT(*) FROM Ordenes", conn
                ).ExecuteScalar();

                model.StockBajo = (int)new SqlCommand(
                    "SELECT COUNT(*) FROM Productos WHERE Stock <= 5 AND Activo = 1", conn
                ).ExecuteScalar();

                model.TotalVentasMonto = Convert.ToDecimal(new SqlCommand(
                    "SELECT ISNULL(SUM(Monto),0) FROM Pagos", conn
                ).ExecuteScalar());
            }

            return model;
        }
    }
}