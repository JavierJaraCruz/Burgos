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
        public int Insertar(Categoria categoria)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Categoria(Nombre,Descripcion,Activo)
                                  VALUES(@Nombre,@Descripcion,@Activo);
                                     SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                cmd.Parameters.AddWithValue("@Activo", categoria.Activo);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Actualizar(Categoria categoria)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Categoria SET Nombre=@Nombre,Descripcion=@Descripcion,
                        Activo=@Activo WHERE Categoria=@Id";
                SqlCommand cmd = new SqlCommand (query,conn);
                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                cmd.Parameters.AddWithValue("@Activo", categoria.Activo);
                cmd.Parameters.AddWithValue("@Id", categoria.CategoriaId);
                conn.Open ();
                cmd.ExecuteNonQuery();
            }

        }
    }
}
