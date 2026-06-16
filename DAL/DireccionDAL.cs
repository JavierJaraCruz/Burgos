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
    public class DireccionDAL
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["burgos"].ConnectionString;

        public List<Direccion> Listar()
        { 
            var lista = new List<Direccion>();

            using (SqlConnection conn = new SqlConnection(ConnectionString)) 
            {
                string query = "SELECT * FROM Direcciones";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    lista.Add(new Direccion
                    
                    {
                        DireccionId = (int)reader["DireccionId"],
                        UsuariId = (int)reader["UsuariId"],
                        Calle = reader["Calle"].ToString(),
                        Ciudad = reader["Ciudad"].ToString(),
                        Pais = reader["Pais"].ToString(),
                        CodigoPostal = reader["CodigoPostal"].ToString()
                    });
                }
            }

            return lista;
        }

        public Direccion ObtenerPorId(int id)
        {
            Direccion direccion = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Direcciones WHERE DireccionId = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    direccion = new Direccion
                    {
                        DireccionId = (int)reader["DireccionId"],
                        UsuariId = (int)reader["UsuariId"],
                        Calle = reader["Calle"].ToString(),
                        Ciudad = reader["Ciudad"].ToString(),
                        Pais = reader["Pais"].ToString(),
                        CodigoPostal = reader["CodigoPostal"].ToString()
                    };
                }
            }

                return direccion;
        }

        public int Insertar(Direccion direccion)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO Direcciones (UsuariId, Calle,Ciudad,Pais,CodigoPostal
                                VALUES(@UsuaiId,@Calle,@Ciudad,@Pais, @CodigoPostal);
                                INSERT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UsuariId", direccion.UsuariId);
                cmd.Parameters.AddWithValue("@Calle", direccion.Calle);
                cmd.Parameters.AddWithValue("@Ciudad", direccion.Ciudad);
                cmd.Parameters.AddWithValue("@Pais", direccion.Pais);
                cmd.Parameters.AddWithValue("@CodigoPostal", direccion.CodigoPostal);
                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());

            }
        }

        public void Actualizar(Direccion direccion) 
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = @"UPDATE Direcciones SET UsuarioId=@UsuarioId,Calle=@Calle,Ciudad=@Ciudad,Pais=@Pais,
                                CodigoPosta=@CodigoPostal WHERE DireccionId=@Id";
                SqlCommand cmd = new SqlCommand (query, conn);
                cmd.Parameters.AddWithValue("@UsuariId", direccion.UsuariId);
                cmd.Parameters.AddWithValue("@Calle", direccion.Calle);
                cmd.Parameters.AddWithValue("@Ciudad", direccion.Ciudad);
                cmd.Parameters.AddWithValue("@Pais", direccion.Pais);
                cmd.Parameters.AddWithValue("@CodigoPostal", direccion.CodigoPostal);
                cmd.Parameters.AddWithValue("@Id", direccion.DireccionId);
                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }
        public void Eliminar(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "DELETE FROM Direcciones WHERE DireccionId=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
