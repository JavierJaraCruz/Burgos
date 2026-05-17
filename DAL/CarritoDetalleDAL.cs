using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CarritoDetalleDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Skart"].ConnectionString;

        public List<CarritoDetalle> Listar() 
        
        { 
            var lista = new List<CarritoDetalle>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CarritoDetalle";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new CarritoDetalle 
                    { 
                        CarritoDetalleId = (int)reader["CarritoDetalleId"],
                        CarritoId = (int)reader["CarritoId"],
                        ProductoId= (int)reader["ProductoId"],
                        Cantidad = (int)reader["Cantidad"],
                        PrecioUnitario = (decimal)reader["PrecioUnitario"],
                        Subtotal = (decimal)reader["SubTotal"]
                    });
                }
            }

            return lista;
        }

        public CarritoDetalle ObtenerPorId(int id)
        {
            CarritoDetalle carritoDetalle = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CarritoDetalle WHERE CarritoDetalleId = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    carritoDetalle = new CarritoDetalle 
                    {
                        CarritoDetalleId = (int)reader["CarritoDetalleId"],
                        CarritoId = (int)reader["CarritoId"],
                        ProductoId = (int)reader["ProductoId"],
                        Cantidad = (int)reader["Cantidad"],
                        PrecioUnitario = (decimal)reader["PrecioUnitario"],
                        Subtotal = (decimal)reader["SubTotal"]
                    };
                }
            }

            return carritoDetalle;
        }
    }
}
