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
    public class CompraProveedorDAL
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Skart"].ConnectionString;

        public List<CompraProveedor> Listar() 
        {
            var lista = new List<CompraProveedor>();
            using (SqlConnection conn = new SqlConnection(ConnectionString)) {
                string query = "SELECT * FROM CompraProveedor";
                SqlCommand cmd = new SqlCommand(query, conn);   
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    lista.Add(new CompraProveedor 
                    
                    { 
                        CompraId = (int)reader["CompraId"],
                        ProveedorId = (int)reader["ProveedorId"],
                        FechaCompra = (DateTime)reader["FechaCompra"],
                        Total = (decimal)reader["Total"],
                        Estado = reader["Estado"].ToString()
                    });
                }
                    
            }



            return lista;
        }

        public CompraProveedor ObtenerPorId(int id) 
        {
            CompraProveedor compraProveedor = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM CompraProveedor WHERE CompraId = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    compraProveedor = new CompraProveedor {
                        CompraId = (int)reader["CompraId"],
                        ProveedorId = (int)reader["ProveedorId"],
                        FechaCompra = (DateTime)reader["FechaCompra"],
                        Total = (decimal)reader["Total"],
                        Estado = reader["Estado"].ToString()
                    };
                }
            }
            return compraProveedor;
        }

        public int Insertar(CompraProveedor cP)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO CompraProveedor (ProveedorId,FechaCompra,Total,Estado)
                                 VALUES(@ProveedorId,@FechaCompra,@Total,@Estado);
                                    SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ProveedorId", cP.ProveedorId);
                cmd.Parameters.AddWithValue("@FechaCompra", cP.FechaCompra);
                cmd.Parameters.AddWithValue("@Total", cP.Total);
                cmd.Parameters.AddWithValue("@Estado", cP.Estado);

                conn.Open() ;

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public void Actualizar(CompraProveedor cP) 
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) 
            
            {
                string query = @"UPDATE CompraProovedor SET ProveedorId=@ProveedorId,FechaCompra=@FechaCompra,Total=@Total,
                                Estado=@Estado WHERE CompraId=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProveedorId", cP.ProveedorId);
                cmd.Parameters.AddWithValue("@FechaCompra", cP.FechaCompra);
                cmd.Parameters.AddWithValue("@Total", cP.Total);
                cmd.Parameters.AddWithValue("@Estado", cP.Estado);
                cmd.Parameters.AddWithValue("@Id", cP.CompraId);
                conn.Open();
                cmd.ExecuteNonQuery();


            }
        }

    }
}
