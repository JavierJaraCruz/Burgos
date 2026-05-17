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
    }
}
