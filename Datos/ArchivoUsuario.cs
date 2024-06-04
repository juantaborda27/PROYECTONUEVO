using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace Datos
{
    public class ArchivoUsuario:BaseDatos
    {
        public void Add(Usuario usuario)
        {
            try
            {
                string query = "INSERT INTO USUARIO (UserName, Cedula, Nombre, Telefono, Contraseña, Rol) VALUES (@UserName, @Cedula, @Nombre, @Telefono, @Contraseña, @Rol)";

                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@UserName", usuario.userName);
                    command.Parameters.AddWithValue("@Cedula", usuario.cedula);
                    command.Parameters.AddWithValue("@Nombre", usuario.nombre);
                    command.Parameters.AddWithValue("@Telefono", usuario.telefono);
                    command.Parameters.AddWithValue("@Contraseña", usuario.contraseña);
                    command.Parameters.AddWithValue("@Rol", usuario.rol);

                    AbrirConexion();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                CerrarConexion();
            }

        }

        public List<Usuario> Leer()
        {
            List<Usuario> usuarioList = new List<Usuario>();
            string Consulta = "SELECT * FROM USUARIO";
            try
            {
                SqlCommand command = new SqlCommand(Consulta, conexion);
                AbrirConexion();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    usuarioList.Add(Map(reader));
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception)
            {
                return null;
            }
            return usuarioList;
        }

        public bool Eliminar(string Cedula)
        {
            string ssql = "DELETE FROM [USUARIO] WHERE [Cedula] = @Cedula";

            using (SqlCommand command = new SqlCommand(ssql, ObtenerConexion()))
            {
                command.Parameters.AddWithValue("@Cedula", Cedula);

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

        public Usuario Buscar(string cedula)
        {
            try
            {
                var usuarios = Leer();
                if (usuarios == null)
                {
                    return null;
                }
                return usuarios.FirstOrDefault(p => p.cedula == cedula);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        public bool Actualizar(Usuario usuarioNew)
        {
            try
            {
                string actualizar = "UPDATE USUARIO SET UserName = @UserName, Nombre = @Nombre, Telefono = @Telefono, Contraseña = @Contraseña, Rol = @Rol WHERE Cedula = @Cedula";
                using (SqlCommand command = new SqlCommand(actualizar, conexion))
                {
                    command.Parameters.AddWithValue("@UserName", usuarioNew.userName);
                    command.Parameters.AddWithValue("@Nombre", usuarioNew.nombre);
                    command.Parameters.AddWithValue("@Telefono", usuarioNew.telefono);
                    command.Parameters.AddWithValue("@Contraseña", usuarioNew.contraseña);
                    command.Parameters.AddWithValue("@Rol", usuarioNew.rol);
                    command.Parameters.AddWithValue("@Cedula", usuarioNew.cedula);

                    AbrirConexion();
                    command.ExecuteNonQuery();
                }
                return true;
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

        private Usuario Map(SqlDataReader reader)
        {
            Usuario usuario = new Usuario
            {
                userName = Convert.ToString(reader["UserName"]),
                cedula = Convert.ToString(reader["Cedula"]),
                nombre = Convert.ToString(reader["Nombre"]),
                telefono = Convert.ToString(reader["Telefono"]),
                contraseña = Convert.ToString(reader["Contraseña"]),
                rol = Convert.ToString(reader["Rol"])
            };

            return usuario;

        }
    }
}
