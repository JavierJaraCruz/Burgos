
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

    }
}
