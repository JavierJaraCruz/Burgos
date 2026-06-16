using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;

namespace DAL
{
    public class RolDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["burgos"].ConnectionString; 

        public List<Rol> Listar()
        {
            var lista = new List<Rol>();
            using (SqlConnection conn = new SqlConnection(connectionString)) {
                string query = "SELECT * FROM Roles";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Rol
                    {
                        RolId = (int)reader["RolId"],
                        NombreRol = reader["NombreRol"].ToString()

                    });
                    
                }

            }


            return lista;
        }
        public Rol ObtenerPorId(int id) 
        
        {
            Rol rol = null;

            using (SqlConnection conn = new SqlConnection(connectionString)) {
                string query = "SELECT * FROM Roles WHERE RolId = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) 
                {
                    rol = new Rol()
                    {
                        RolId = (int)reader["RolId"],
                        NombreRol = reader["NombreRol"].ToString()
                    };
                }
            }



            return rol;
        }

        public int Insertar(Rol r)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Roles (NombreRol) VALUES (
                    @NombreRol);
                    SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@NombreRol", r.NombreRol);
                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Actualizar(Rol rol) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                string query = "UPDATE Roles SET NombreRol=@NomR WHERE RolId=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@NomR", rol.NombreRol);
                cmd.Parameters.AddWithValue("@Id", rol.RolId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Eliminar(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Roles WHERE RolId=@Id";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }


    
}
