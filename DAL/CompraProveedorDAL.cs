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

    }
}
