using System.Data.SqlClient;
using System.Data;

namespace D_AlturaSystemAPI.Servicio
{
    public class ServiciosBD(IConfiguration configuration)
    {
        private readonly IConfiguration confi = configuration;

        public DataTable ObtenerDatosProveedor()
        {
            DataTable dt = new();

            string query = "SELECT * FROM vProveedor";

            string conexion = confi.GetConnectionString("ConnectSQL");

            using (SqlConnection conn = new(conexion))
            {
                using SqlCommand cmd = new (query, conn);
                conn.Open();

                using SqlDataAdapter Da = new(cmd);
                Da.Fill(dt);
            }

            return dt;
        }
    }
}