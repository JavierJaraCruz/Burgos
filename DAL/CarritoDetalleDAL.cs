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
    public class CarritoDetalleDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["burgos"].ConnectionString;

        public List<CarritoDetalle> Listar() 
        { 
            var lista = new List<CarritoDetalle>();
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CarritoDetalle";
                SqlCommand cmd = new SqlCommand(query,conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new CarritoDetalle
                    {
                        CarritoDetalleId = (int)reader["CarritoDetalleId"],
                        CarritoId = (int)reader["CarritoId"],
                        ProductoId = (int)reader["ProductoIds"],
                        Cantidad = (int)reader["Cantidad"],
                        PrecioUnitario = (decimal)reader["PrecioUnitario"],
                        Subtotal = (decimal)reader["SubTotal"]
                    });
                }

            }
            return lista;
        }
        public CarritoDetalle ObtenerPorId(int id) 
        {
            CarritoDetalle carritoDetalle = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CarritoDetalle WHERE CarritoDetalleId = @id";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   carritoDetalle = new CarritoDetalle
                    {
                        CarritoDetalleId = (int)reader["CarritoDetalleId"],
                        CarritoId = (int)reader["CarritoId"],
                        ProductoId = (int)reader["ProductoIds"],
                        Cantidad = (int)reader["Cantidad"],
                        PrecioUnitario = (decimal)reader["PrecioUnitario"],
                        Subtotal = (decimal)reader["SubTotal"]

                    };
                }
            }
            return carritoDetalle;
        }

        public int Insertar(CarritoDetalle carritoDetalle) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO CarritoDetalle(CarritoId,ProductoId,Cantidad,PrecioUnitario,SubTotal) VALUES(@CarritoId,@ProductoId,@Cantidad,@PrecioUnitario,@SubTotal);
                                SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@Carritoid", carritoDetalle.CarritoId);
                cmd.Parameters.AddWithValue("@ProductoId", carritoDetalle.ProductoId);
                cmd.Parameters.AddWithValue("@Cantidad", carritoDetalle.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", carritoDetalle.PrecioUnitario);
                cmd.Parameters.AddWithValue("@SubTotal", carritoDetalle.Subtotal);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());

            }
        }
        public void Actualizar(CarritoDetalle carritoDetalle)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE CarritoDetalle SET CarritoId=@CarritoId,ProductoId=@ProductoId,Cantidad=@Cantidad,
                                 PrecioUnitario=@PrecioUnitario,SubTotal=@SubTotal WHERE CarritoDetalleId=@Id";
                SqlCommand cmd= new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@Carritoid", carritoDetalle.CarritoId);
                cmd.Parameters.AddWithValue("@ProductoId", carritoDetalle.ProductoId);
                cmd.Parameters.AddWithValue("@Cantidad", carritoDetalle.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", carritoDetalle.PrecioUnitario);
                cmd.Parameters.AddWithValue("@SubTotal", carritoDetalle.Subtotal);
                cmd.Parameters.AddWithValue("@Id", carritoDetalle.CarritoDetalleId);
                conn.Open() ;   
                cmd.ExecuteNonQuery();

            }

        }

        public void Eliminar(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM CarritoDetalle WHERE CarritoDetalleId=@Id";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
    }
}
