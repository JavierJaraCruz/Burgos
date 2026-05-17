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
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Skart"].ConnectionString;

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


    }
}
