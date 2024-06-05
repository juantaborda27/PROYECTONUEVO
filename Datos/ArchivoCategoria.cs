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
    public class ArchivoCategoria: BaseDatos
    {
        public void Add(CategoriaProducto categoriaProducto)
        {
            try
            {
                string registro = "INSERT INTO categoria (IdCategoria, DescripcionCategoria) VALUES (@IdCategoria, @DescripcionCategoria)";

                using (SqlCommand command = new SqlCommand(registro, conexion))
                {
                    command.Parameters.AddWithValue("@IdCategoria", categoriaProducto.idCategoria);
                    command.Parameters.AddWithValue("@DescripcionCategoria", categoriaProducto.descripcion);

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

        public List<CategoriaProducto> Leer()
        {
            List<CategoriaProducto> categoriaList = new List<CategoriaProducto>();
            string consulta = "SELECT * FROM categoria";
            try
            {
                SqlCommand command = new SqlCommand(consulta, conexion);
                AbrirConexion();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categoriaList.Add(Map(reader));
                    }
                }
                CerrarConexion();
            }
            catch (Exception)
            {
                return null;
            }
            return categoriaList;
        }

        public bool Eliminar(int IdCategoria)
        {
            string ssql = "DELETE FROM [categoria] WHERE [IdCategoria] = @IdCategoria";

            try
            {
                using (SqlCommand command = new SqlCommand(ssql, ObtenerConexion()))
                {
                    command.Parameters.AddWithValue("@IdCategoria", IdCategoria);

                    AbrirConexion();
                    var i = command.ExecuteNonQuery(); 
                    CerrarConexion();

                    if (i > 0)
                    {
                        return true;
                    }
                    return false;
                }
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

        public CategoriaProducto Buscar(int IdCategoria)
        {
            try
            {
                var categorias = Leer();
                if (categorias == null)
                {
                    return null;
                }
                return categorias.FirstOrDefault(p => p.idCategoria== IdCategoria);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Actualizar(CategoriaProducto categoriaProductoNew)
        {
            try
            {
                string Actualizar = "ModificarCategoria";
                SqlCommand command = new SqlCommand(Actualizar, conexion);
                command.Parameters.AddWithValue("@IdCategoria", categoriaProductoNew.idCategoria);
                command.Parameters.AddWithValue("@TipoCategoria", categoriaProductoNew.descripcion);
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

        private CategoriaProducto Map(SqlDataReader reader)
        {
            CategoriaProducto categoria = new CategoriaProducto
            {
                idCategoria = Convert.ToInt32(reader["IdCategoria"]),
                descripcion = Convert.ToString(reader["DescripcionCategoria"]),
            };

            return categoria;
        }
    }
}
