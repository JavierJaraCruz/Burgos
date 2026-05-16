using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration; 
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;

namespace DAL
{
    public class OrdenDetalleDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Skart"].ConnectionString;

        public List<OrdenDetalle> Listar() 
        {
            
            var lista = new List<OrdenDetalle>();

            using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                string query = "SELECT * FROM OrdenDetalle";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    lista.Add(new OrdenDetalle { 
                        OrdenDetalleId = (int)reader["OrdenDetalleId"],
                        OrdenId = (int)reader["OrdenId"],
                        ProductoId = (int)reader["ProductoId"],
                        Cantidad = (int)reader["Cantidad"],
                        PrecioUnitario = (decimal)reader["PrecioUnitario"],
                        Subtotal = (decimal)reader["SubTotal"]
                    
                    });
                
                }
            
            }



                return lista;
        }

        public OrdenDetalle ObtenerPorId(int id) 
        
        {
            OrdenDetalle ordenDetalle = null;

            using(SqlConnection conn = new SqlConnection(connectionString)) 
            {
                string query = " SELECT * FROM OrdenDetalle WHERE OrdenDetalleId = @id";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ordenDetalle = new OrdenDetalle
                    {
                        OrdenDetalleId = (int)reader["OrdenDetalleId"],
                        OrdenId = (int)reader["OrdenId"],
                        ProductoId = (int)reader["ProductoId"],
                        Cantidad = (int)reader["Cantidad"],
                        PrecioUnitario = (decimal)reader["PrecioUnitario"],
                        Subtotal = (decimal)reader["SubTotal"]
                    };
                }
            }

            return ordenDetalle;
        }

    }
}
