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
            string registro = "INSERT INTO CLIENTE (DocumentoCliente, NombreCliente, Correo, Telefono) VALUES (@Documento, @NombreCliente, @Correo, @Telefono)";

            try
            {
                using (SqlCommand command = new SqlCommand(registro, conexion))
                {
                    command.Parameters.AddWithValue("@Documento", cliente.Documento);
                    command.Parameters.AddWithValue("@NombreCliente", cliente.NombreCliente);
                    command.Parameters.AddWithValue("@Correo", cliente.Correo);
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono);

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

        public bool Eliminar(string Documento)
        {
            try
            {
                string Eliminar = "EliminarCliente";

                SqlCommand command = new SqlCommand(Eliminar, conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DocumentoCliente", Documento);
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

        public Cliente Buscar(string Documento)
        {
            return Leer().FirstOrDefault(p => p.Documento == Documento);
        }

        public bool Actualizar(Cliente clienteNew)
        {
            try
            {
                string Actualizar = "ModificarCliente";
                SqlCommand command = new SqlCommand(Actualizar, conexion);
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
