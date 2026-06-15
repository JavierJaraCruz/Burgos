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

        public int Insertar(OrdenDetalle oD)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            { 
                string query = @"INSERT INTO OrdenDetalle (OrdenId,ProductoId,Cantidad,PrecioUnitario,Subtotal)" +
                    "VALUES (@OrdenId,@ProductoId,@Cantidad,@PrecioUnitario,@SubTotal);" +
                    "" +
                    "SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@OrdenId", oD.OrdenId);
                cmd.Parameters.AddWithValue("@ProductoId", oD.ProductoId);
                cmd.Parameters.AddWithValue("@Cantidad", oD.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", oD.PrecioUnitario);
                cmd.Parameters.AddWithValue("@SubTotal", oD.Subtotal);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public void Actualizar(OrdenDetalle ordenD)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE OrdenDetalle SET OrdenId=@OrdenId,ProductoId=@ProductoId,Cantidad=@Cantidad,
                            PrecioUnitario=@PrecioUnitario,SubTotal=@SubTotal WHERE OrdenDetalleId=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                
                cmd.Parameters.AddWithValue("@OrdenId", ordenD.OrdenId);
                cmd.Parameters.AddWithValue("@ProductoId", ordenD.ProductoId);
                cmd.Parameters.AddWithValue("@Cantidad", ordenD.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", ordenD.PrecioUnitario);
                cmd.Parameters.AddWithValue("@SubTotal", ordenD.Subtotal);
                cmd.Parameters.AddWithValue("@Id", ordenD.OrdenDetalleId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
