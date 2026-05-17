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
    public class CategoriaDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Skart"].ConnectionString;

        public List<Categoria> Listar()
        { 
            var lista = new List<Categoria>();

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Categorias";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                 SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Categoria 
                    { 
                        CategoriaId = (int)reader["CategoriaId"],
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        Activo = (bool)reader["Activo"]
                    });
                }
            }

            return lista;
        }

        public Categoria ObtenerPorId(int id) 
        {
            Categoria categoria = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Categorias WHERE CategoriaId = @id";
                SqlCommand cmd = new SqlCommand(query,conn);

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    categoria = new Categoria 
                    {
                        CategoriaId = (int)reader["CategoriaId"],
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        Activo = (bool)reader["Activo"]
                    };
                }
                  
            }
                


            return categoria;
        }
    }
}
