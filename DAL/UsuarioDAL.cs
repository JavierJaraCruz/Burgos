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
    public class UsuarioDAL
    {       //Preparar la cadema de conexion o TEXTO DE CONEXION
        private readonly string connectionString =  ConfigurationManager.ConnectionStrings["burgos"].ConnectionString;


        //creamos el metodo o funcion a utilizar del tipo List porque devolveremos una lista
        public List<Usuario> Listar() {
            //creamos la lista
            var lista = new List<Usuario>();
            //preparar lo que se va listar
            //usando el using
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //creamos el string que tendra la query
                string query = "SELECT * FROM Usuarios";
                //vincular el query a una conexion
                SqlCommand cmd = new SqlCommand(query, conn);
                //abrir la conexion
                conn.Open();
                //preparamos el cursor
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Usuario 
                    { 
                        UsuarioId = (int)reader["UsuarioId"],
                        NombreUsuario = reader["NombreUsuario"].ToString(),
                        Email = reader["Email"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Salt = reader["Salt"].ToString(),
                        FechaRegistro = (DateTime)reader["FechaRegistro"],
                        Estado = (bool)reader["Estado"]
                    
                    
                    });
                }





            }


                return lista;
        }


        public Usuario ObtenerPorId(int id)
        {
            Usuario usuario = null;
            using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                string query = "SELECT * FROM Usuarios WHERE UsuarioId=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) 
                {
                    usuario = new Usuario
                    {
                        UsuarioId = (int)reader["UsuarioId"],
                        NombreUsuario = reader["NombreUsuario"].ToString(),
                        Email = reader["Email"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Salt = reader["Salt"].ToString(),
                        FechaRegistro = (DateTime)reader["FechaRegistro"],
                        Estado = (bool)reader["Estado"]

                    };

                }

            }

                return usuario;
        }

        //insertar
        public int Insertar(Usuario u)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Usuarios (NombreUsuario,Email,
                    PasswordHash,Salt,FechaRegistro,Estado) VALUES (@NombreUsuario,@Email,@PasswordHash,
                    @Salt,@FechaRegistro,@Estado); 
                                 SELECT SCOPE_IDENTITY();";
                SqlCommand cmd= new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@NombreUsuario", u.NombreUsuario);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", u.PasswordHash);
                cmd.Parameters.AddWithValue("@Salt", u.Salt);
                cmd.Parameters.AddWithValue("@FechaRegistro", u.FechaRegistro);
                cmd.Parameters.AddWithValue("@Estado", u.Estado);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Actualizar(Usuario usuario) 
        {
            using(SqlConnection conn = new SqlConnection(connectionString)) 
            {
                string query = @"UPDATE Usuarios SET NombreUsuario=@Nom,Email=@Ema,PasswordHash=@Pass,Salt=@Sal
                                       FechaRegistro=@Fecha,Estado=@Estad WHERE UsuarioId=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nom", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@Ema", usuario.Email);
                cmd.Parameters.AddWithValue("@Pass", usuario.PasswordHash);
                cmd.Parameters.AddWithValue("@Sal", usuario.Salt);
                cmd.Parameters.AddWithValue("@Fecha", usuario.FechaRegistro);
                cmd.Parameters.AddWithValue("@Estad", usuario.Estado);
                cmd.Parameters.AddWithValue("@Id", usuario.UsuarioId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        { 
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Usuarios WHERE UsuarioId=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
