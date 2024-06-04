using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ArchivoProveedor:BaseDatos
    {
        int index = 0;
        public void Add(Proveedor proveedor)
        {

            string registro = "INSERT INTO PROVEEDOR (DocumentoProveedor, NombreProveedor, Telefono) VALUES (@Documento, @NombreProveedor, @Telefono)";

            try
            {
                using (SqlCommand command = new SqlCommand(registro, conexion))
                {
                    command.Parameters.AddWithValue("@Documento", proveedor.documento);
                    command.Parameters.AddWithValue("@NombreProveedor", proveedor.NombreProveedor);
                    command.Parameters.AddWithValue("@Telefono", proveedor.telefono);

                    AbrirConexion();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }


        public List<Proveedor> Leer()
        {
            List<Proveedor> proveedorList = new List<Proveedor>();
            string Consulta = "SELECT * FROM PROVEEDOR";
            try
            {
                SqlCommand command = new SqlCommand(Consulta, conexion);
                AbrirConexion();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    proveedorList.Add(Map(reader));
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception)
            {
                return null;
            }
            return proveedorList;
        }

        public bool Eliminar(string documento)
        {
            string eliminar = "DELETE FROM PROVEEDOR WHERE DocumentoProveedor = @Documento";

            try
            {
                using (SqlCommand command = new SqlCommand(eliminar, conexion))
                {
                    command.Parameters.AddWithValue("@Documento", documento);

                    AbrirConexion();
                    int rowsAffected = command.ExecuteNonQuery();
                    CerrarConexion();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }

        public Proveedor Buscar(string IdProveedor)
        {
            try
            {
                var proveedores = Leer();
                if (proveedores == null)
                {
                    return null;
                }
                return proveedores.FirstOrDefault(p => p.idProveedor == IdProveedor);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Actualizar(Proveedor proveedorNew)
        {
            try
            {
                string Actualizar = "ModificarProveedor";
                SqlCommand command = new SqlCommand(Actualizar, conexion);
                command.Parameters.AddWithValue("@DocumentoProveedor", proveedorNew.documento);
                command.Parameters.AddWithValue("@NombreProveedor", proveedorNew.NombreProveedor);
                command.Parameters.AddWithValue("@Telefono", proveedorNew.telefono);
                command.CommandType = CommandType.StoredProcedure;
                AbrirConexion();
                var index = command.ExecuteNonQuery();
                CerrarConexion();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private Proveedor Map(SqlDataReader reader)
        {
            Proveedor proveedor = new Proveedor
            {
                idProveedor = Convert.ToString(reader["IdProveedor"]),
                documento = Convert.ToString(reader["DocumentoProveedor"]),
                NombreProveedor = Convert.ToString(reader["NombreProveedor"]),
                telefono = Convert.ToString(reader["Telefono"])
            };

            return proveedor;
        }
    }
}
