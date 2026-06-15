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
    public class CarritoDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Skart"].ConnectionString;

        public List<Carrito> Listar()
        { 
            var lista = new List<Carrito>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Carrito";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Carrito
                    { 
                        CarritoId = (int)reader["CarritoId"],
                        UsuarioId = (int)reader["UsuarioId"],
                        FechaCreacion = (DateTime)reader["FechaCreacion"],
                        Estado = reader["Estado"].ToString()
                    });
                }
            }

                return lista;
        }

        public Carrito ObtenerPorId(int id)
        { 
            Carrito carrito = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Carrito WHERE CarritoId = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    carrito = new Carrito 
                    {
                        CarritoId = (int)reader["CarritoId"],
                        UsuarioId = (int)reader["UsuarioId"],
                        FechaCreacion = (DateTime)reader["FechaCreacion"],
                        Estado = reader["Estado"].ToString()
                    };
                }
            }

                return carrito;
        }
        public int Insertar(Carrito carrito) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Carrito(UsuarioId,FechaCreacion,Estado)
                                VALUES(@UsuarioId,@FechaCreacion,@Estado);
                                SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand (query, conn);
                cmd.Parameters.AddWithValue("@UsuarioId", carrito.UsuarioId);
                cmd.Parameters.AddWithValue("@FechaCreacion", carrito.FechaCreacion);
                cmd.Parameters.AddWithValue("@Estado", carrito.Estado);
                conn.Open ();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public void Actualizar(Carrito carrito)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Carrito SET UsuarioId=@UsuarioId,FechaCreacion=@FechaCreacion,
                                    Estado=@Estado WHERE CarritoId=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UsuarioId", carrito.UsuarioId);
                cmd.Parameters.AddWithValue("@FechaCreacion", carrito.FechaCreacion);
                cmd.Parameters.AddWithValue("@Estado", carrito.Estado);
                cmd.Parameters.AddWithValue("@Id", carrito.CarritoId);
                conn.Open ();
                cmd.ExecuteNonQuery ();
            }

        }
    }
}
