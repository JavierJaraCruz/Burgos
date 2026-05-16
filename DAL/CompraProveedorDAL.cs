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

    }
}
