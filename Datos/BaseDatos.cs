using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class BaseDatos
    {
        private string cadenaConexion = "Server=.\\SQLEXPRESS;Database=PROYECTOTVV_DB;Trusted_Connection=True;";
        protected SqlConnection conexion;
        public BaseDatos()
        {
            conexion = new SqlConnection(cadenaConexion);
        }
        public bool AbrirConexion()
        {
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
                return true;
            }
            return false;
        }

        public void CerrarConexion()
        {
            conexion.Close();
        }

        public SqlConnection ObtenerConexion()
        {
            return conexion;
        }
    }
}
