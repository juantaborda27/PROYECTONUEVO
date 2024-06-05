using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ArchivoCliente:BaseDatos
    {
        public void Add(Cliente cliente)
        {
            try
            {
                string registro = "INSERT INTO CLIENTE (DocumentoCliente,NombreCliente,Correo,Telefono) VALUES (@DocumentoCliente,@NombreCliente,@Correo,@Telefono)";

                using (SqlCommand command = new SqlCommand(registro, conexion))
                {
                    command.Parameters.AddWithValue("@DocumentoCliente", cliente.Documento);
                    command.Parameters.AddWithValue("@NombreCliente", cliente.NombreCliente);
                    command.Parameters.AddWithValue("@Correo", cliente.Correo);
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                    AbrirConexion();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                CerrarConexion();
            }
        }

        public List<Cliente> Leer()
        {
            List<Cliente> clienteList = new List<Cliente>();
            string Consulta = "SELECT * FROM CLIENTE";
            try
            {
                SqlCommand command = new SqlCommand(Consulta, conexion);
                AbrirConexion();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    clienteList.Add(Map(reader));
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception)
            {
                return null;
            }
            return clienteList;
        }

        public bool Eliminar(string documento)
        {
            string ssql = "DELETE FROM [CLIENTE] WHERE [DocumentoCliente] = @DocumentoCliente";

            using (SqlCommand command = new SqlCommand(ssql, ObtenerConexion()))
            {
                command.Parameters.AddWithValue("@DocumentoCliente", documento);

                try
                {
                    AbrirConexion();
                    var i = command.ExecuteNonQuery(); // insert, update y delete
                    CerrarConexion();

                    if (i > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    CerrarConexion();
                }
            }
        }

        public Cliente Buscar(string Documento)
        {
            try
            {
                var clientes = Leer();
                if (clientes == null)
                {
                    return null;
                }
                return clientes.FirstOrDefault(p => p.Documento == Documento);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Actualizar(Cliente clienteNew)
        {
            try
            {
                string query = "UPDATE FROM Clientes WHERE IdCliente = @IdCliente";
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@NombreCliente", clienteNew.NombreCliente);
                command.Parameters.AddWithValue("@Correo", clienteNew.Correo);
                command.Parameters.AddWithValue("@Telefono", clienteNew.Telefono);
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

        private Cliente Map(SqlDataReader reader)
        {
            Cliente cliente = new Cliente
            {
                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                Documento = Convert.ToString(reader["DocumentoCliente"]),
                NombreCliente = Convert.ToString(reader["NombreCliente"]),
                Correo = Convert.ToString(reader["Correo"]),
                Telefono = Convert.ToString(reader["Telefono"]),
            };

            return cliente;
        }
    }
}
