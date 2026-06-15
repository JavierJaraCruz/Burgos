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
    public class OrdenDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Skart"].ConnectionString;

        public List<Orden> Listar() 
        {
            var lista = new List<Orden>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ¨* FROM Ordenes";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Orden { 
                        OrdenId = (int)reader["OrdenId"],
                        UsuarioId = (int)reader["UsuarioId"],
                        FechaOrden = (DateTime)reader["FechaOrden"],
                        Total = (decimal)reader["Total"],
                        Estado = reader["Estado"].ToString()
                    });
                
                }
            }


            return lista;
        }

        public Orden ObtenerPorId(int id) 
        {
            Orden orden = null;

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Ordenes WHERE OrdenId = @id ";
                SqlCommand cmd = new SqlCommand(@query, conn);

                cmd.Parameters.AddWithValue("id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    orden = new Orden
                    {
                        OrdenId = (int)reader["OrdenId"],
                        UsuarioId = (int)reader["UsuarioId"],
                        FechaOrden = (DateTime)reader["FechaOrden"],
                        Total = (decimal)reader["Total"],
                        Estado = reader["Estado"].ToString()
                    };
                }
            }

            return orden;
        }

        public int Insertar(Orden o) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Ordenes (UsuarioId,FechaOrden,Total, Estado)" +
                    " VALUES (@UsuarioId,@FechaOrden, @Total, @Estado);" +
                    "SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UsuarioId", o.UsuarioId);
                cmd.Parameters.AddWithValue("@FechaOrden", o.FechaOrden);
                cmd.Parameters.AddWithValue("@Total", o.Total);
                cmd.Parameters.AddWithValue("@Estado", o.Estado);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public void Actualizar(Orden orden)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Ordenes SET UsuarioId=@UsuarioId,FechaOrden=@FechaOrden,Total=@Total,
                                Estado=@Estado WHERE OrdenId=@Id";
                SqlCommand cmd = new SqlCommand (query, conn);

                cmd.Parameters.AddWithValue("@UsuarioId", orden.UsuarioId);
                cmd.Parameters.AddWithValue("@FechaOrden", orden.FechaOrden);
                cmd.Parameters.AddWithValue("@Total", orden.Total);
                cmd.Parameters.AddWithValue("@Estado", orden.Estado);
                cmd.Parameters.AddWithValue("@Id", orden.OrdenId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

            
    }
}
