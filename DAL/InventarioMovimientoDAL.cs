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
    public class InventarioMovimientoDAL
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Skart"].ConnectionString;

        public List<InventarioMovimiento> Listar()
        {
            var lista = new List<InventarioMovimiento>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM InventarioMovimiento";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new InventarioMovimiento

                    {
                        MovimientoId = (int)reader["MovimientoId"],
                        ProductoId = (int)reader["ProductoId"],
                        TipoMovimiento = reader["TipoMovimiento"].ToString(),
                        Cantidad = (int)reader["Cantidad"],
                        FechaMovimiento = (DateTime)reader["FechaMovimiento"],
                        Referencia = reader["Referencia"].ToString()

                    });
                }
            }

            return lista;
        }

        public InventarioMovimiento ObtenerPorId(int id)
        {
            InventarioMovimiento inventarioMovimiento = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM InventarioMovimiento WHERE MovimientoId = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    inventarioMovimiento = new InventarioMovimiento
                    {
                        MovimientoId = (int)reader["MovimientoId"],
                        ProductoId = (int)reader["ProductoId"],
                        TipoMovimiento = reader["TipoMovimiento"].ToString(),
                        Cantidad = (int)reader["Cantidad"],
                        FechaMovimiento = (DateTime)reader["FechaMovimiento"],
                        Referencia = reader["Referencia"].ToString()
                    };
                }
            }


            return inventarioMovimiento;
        }

        public int Insertar(InventarioMovimiento iM)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO InventarioMovimiento(ProductoId, TipoMovimiento, Cantidad, FechaMovimiento, Referencia)" +
                    "VALUES (@ProductoId, @TipoMovimiento,@Cantidad,@FechaMovimiento, @Referencia);" +
                    "SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ProductoId", iM.ProductoId);
                cmd.Parameters.AddWithValue("@TipoMovimiento", iM.TipoMovimiento);
                cmd.Parameters.AddWithValue("@Cantidad", iM.Cantidad);
                cmd.Parameters.AddWithValue("@FechaMovimiento", iM.FechaMovimiento);
                cmd.Parameters.AddWithValue("@Referencia", iM.Referencia);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());


            }
        }
        public void Actualizar(InventarioMovimiento inventarioM)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = @"UPDATE InventarioMovimiento SET ProductoId=@ProductoId,TipoMovimiento=@TipoMovimiento,Cantidad=@Cantidad
                                FechaMovimiento=@FechaMovimiento,Referencia=@Referencia WHERE MovimientoId=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                
                cmd.Parameters.AddWithValue("@ProductoId", inventarioM.ProductoId);
                cmd.Parameters.AddWithValue("@TipoMovimiento", inventarioM.TipoMovimiento);
                cmd.Parameters.AddWithValue("@Cantidad", inventarioM.Cantidad);
                cmd.Parameters.AddWithValue("@FechaMovimiento", inventarioM.FechaMovimiento);
                cmd.Parameters.AddWithValue("@Referencia", inventarioM.Referencia);
                cmd.Parameters.AddWithValue("@Id", inventarioM.MovimientoId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
