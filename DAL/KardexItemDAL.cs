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
    public class KardexItemDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Skart"].ConnectionString;

        public List<KardexItem> Listar()
        {
            var lista = new List<KardexItem>();

            using(SqlConnection conn = new SqlConnection(connectionString)) 
            { 
                string query = "SELECT * FROM "
            }

            return lista;
        }
    
    
    
    }

}
