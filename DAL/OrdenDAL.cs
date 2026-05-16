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
    public class OrdenDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Skart"].ConnectionString;

        public List<Orden> Listar() 
        {
            var lista = new List<Orden>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ¨* FROM Ordenes";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Orden { 
                        OrdenId = (int)reader["OrdenId"],
                        UsuarioId = (int)reader["UsuarioId"],
                        FechaOrden = (DateTime)reader["FechaOrden"],
                        Total = (decimal)reader["Total"],
                        Estado = reader["Estado"].ToString()
                    });
                
                }
            }


            return lista;
        }

            
    }
}
