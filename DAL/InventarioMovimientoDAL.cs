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
                SqlCommand cmd = new SqlCommand (query, conn);

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

    }
}
