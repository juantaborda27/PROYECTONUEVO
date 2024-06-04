using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ArchivoProducto:BaseDatos
    {
        ArchivoCategoria archivoCategoria = new ArchivoCategoria();

        public void Add(Producto producto)
        {
            string registro = "INSERT INTO PRODUCTO (IdProducto, DescripcionProducto, IdCategoria, CantidadMinima) VALUES (@IdProducto, @DescripcionProducto, @IdCategoria, @CantidadMinima)";

            try
            {
                using (SqlCommand command = new SqlCommand(registro, conexion))
                {
                    command.Parameters.AddWithValue("@IdProducto", producto.idProducto);
                    command.Parameters.AddWithValue("@DescripcionProducto", producto.descripcion);
                    command.Parameters.AddWithValue("@IdCategoria", producto.categoriaProducto.idCategoria);
                    command.Parameters.AddWithValue("@CantidadMinima", producto.cantidadMinima);

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

        public List<Producto> Leer()
        {
            List<Producto> productoList = new List<Producto>();
            string Consulta = "SELECT * FROM PRODUCTO";
            try
            {
                SqlCommand command = new SqlCommand(Consulta, conexion);
                AbrirConexion();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    productoList.Add(Map(reader));
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception)
            {
                return null;
            }
            return productoList;
        }

        public bool Eliminar(string idProducto)
        {
            string query = "DELETE FROM PRODUCTO WHERE IdProducto = @IdProducto";

            try
            {
                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@IdProducto", idProducto);

                    AbrirConexion();
                    var rowsAffected = command.ExecuteNonQuery();
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

        public Producto Buscar(string idProducto)
        {
            try
            {
                var productos = Leer();
                if (productos == null)
                {
                    return null;
                }
                return productos.FirstOrDefault(p => p.idProducto == idProducto);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Actualizar(Producto productoNew)
        {
            string query = "UPDATE FROM PRODUCTO SET (PrecioCompra, PrecioVenta, CantidadExistente) WHERE IdProducto = @IdProducto";
            try
            {
                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@IdProducto", productoNew.idProducto);
                    command.Parameters.AddWithValue("@PrecioCompra", productoNew.precioCompra);
                    command.Parameters.AddWithValue("@PrecioVenta", productoNew.precioVenta);
                    command.Parameters.AddWithValue("@CantidadExistente", productoNew.cantidad);
                    AbrirConexion();
                    var rowsAffected = command.ExecuteNonQuery();
                    CerrarConexion();

                    return rowsAffected > 0;
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


        private Producto Map(SqlDataReader reader)
        {
            Producto producto = new Producto
            {
                idProducto = Convert.ToString(reader["IdProducto"]),
                descripcion = Convert.ToString(reader["DescripcionProducto"]),
                cantidad = Convert.ToInt16(reader["CantidadExistente"]),
                precioCompra = Convert.ToDouble(reader["PrecioCompra"]),
                precioVenta = Convert.ToDouble(reader["PrecioVenta"]),
                cantidadMinima = Convert.ToInt32(reader["CantidadMinima"])
            };
            int IdCategoria = Convert.ToInt32(reader["IdCategoria"]);
            producto.categoriaProducto = ObtenerCategoria(IdCategoria);

            return producto;
        }

        private CategoriaProducto ObtenerCategoria(int IdCategoria)
        {
            return archivoCategoria.Leer().Find(c => c.idCategoria == IdCategoria);
        }
    }
}
