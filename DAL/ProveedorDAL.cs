
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

using Entities;


namespace DAL
{
    public class ProveedorDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SkartDB"].ConnectionString;


        public List<Proveedor> Listar()
        {
            var lista = new List<Proveedor>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Proveedores";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Proveedor
                    {
                        ProveedorId = (int)reader["ProveedorId"],
                        Nombre = reader["Nombre"].ToString(),
                        Email = reader["Email"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Direccion = reader["Direccion"].ToString()
                    });
                }
            }
            return lista;
        }

        public Proveedor ObtenerPorId(int id) 
        { 
            Proveedor proveedor = null;

                using (SqlConnection conn = new SqlConnection(connectionString)) 
                {
                    string query = "SELECT * FROM Proveedores WHERE ProveedorId = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                        
                    if(reader.Read())
                    {
                        proveedor = new Proveedor 
                        {
                            ProveedorId = (int)reader["ProveedorId"],
                            Nombre = reader["Nombre"].ToString(),
                            Email = reader["Email"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Direccion = reader["Direccion"].ToString()

                        };
                    }
                }

            return proveedor;
        }

        public int Insertar(Proveedor p)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSER INTO Proveedores (Nombre,Email,Telefono,Direccion)" +
                    "             Values (@Nombre,@Email, @Telefono,@Direccion); " +
                    "           SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query,conn);

                cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@Email", p.Email);
                cmd.Parameters.AddWithValue("@Telefono", p.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", p.Direccion);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
                
        }

        public void Actualizar(Proveedor proveedor) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Proveedores SET Nombre=@Nom,Email=@Ema,Telefono=@Tel,Direccion=@Direc,
                                WHERE ProveedorId=@Id";
                SqlCommand cmd= new SqlCommand(query,conn);

                cmd.Parameters.AddWithValue("@Nom", proveedor.Nombre);
                cmd.Parameters.AddWithValue("@Ema", proveedor.Email);
                cmd.Parameters.AddWithValue("@Tel", proveedor.Email);
                cmd.Parameters.AddWithValue("@Direc", proveedor.Direccion);
                cmd.Parameters.AddWithValue("@Id", proveedor.ProveedorId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
