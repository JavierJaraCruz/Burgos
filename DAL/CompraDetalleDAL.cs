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
    public class CompraDetalleDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Skart"].ConnectionString;

        public List<CompraDetalle> Listar() { 
            var lista = new List<CompraDetalle>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CompraDetalle";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    lista.Add(new CompraDetalle 
                    { 
                        CompraDetalleId = (int)reader["CompraDetalleId"],
                        CompraId = (int)reader["CompraId"],
                        ProductoId = (int)reader["ProductoId"],
                        Cantidad = (int)reader["Cantidad"],
                        PrecioUnitario = (decimal)reader["PrecioUnitario"],
                        Subtotal = (decimal)reader["SubTotal"]
                    });
                } 
                    
            }

            return lista;
        }

        public CompraDetalle ObtenerPorId(int id)
        {
            CompraDetalle compraDetalle = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CompraDetalle WHERE CompraDetalleId = @id";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    compraDetalle = new CompraDetalle
                    {
                        CompraDetalleId = (int)reader["CompraDetalleId"],
                        CompraId = (int)reader["CompraId"],
                        ProductoId = (int)reader["ProductoId"],
                        Cantidad = (int)reader["Cantidad"],
                        PrecioUnitario = (decimal)reader["PrecioUnitario"],
                        Subtotal = (decimal)reader["SubTotal"]
                    };
                }
            }
                


            return compraDetalle;        }

    }


}
