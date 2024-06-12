using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Datos
{
    public class ArchivoCompra : BaseDatos
    {
        ArchivoProveedor archivoProveedor = new ArchivoProveedor();
        ArchivoProducto archivoProducto = new ArchivoProducto();
        ArchivoUsuario archivoUsuario = new ArchivoUsuario();
        public void Add(Compra compra)
        {
            string registroCompra = "INSERT INTO COMPRA (IdCompra,IdUsuario,IdProveedor,MontoTotal) VALUES " +
                "(@IdCompra,@IdUsuario, @IdProveedor, @MontoTotal)";
            string registroDetalleCompra = "INSERT INTO DETALLECOMPRA (IdCompra,IdProducto,PrecioCompra,PrecioVenta,CantidadProducto,SubTotal) VALUES " +
                "(@IdCompra, @IdProducto, @PrecioCompra,@PrecioVenta,@CantidadProducto,@SubTotal)";

            SqlTransaction accion = null;

            try
            {
                AbrirConexion();
                //Una transacción es una secuencia de operaciones de base de datos que se ejecutan de forma atómica. 
                accion = conexion.BeginTransaction();
                using (SqlCommand command = new SqlCommand(registroCompra, conexion, accion))
                {
                    command.Parameters.AddWithValue("@IdCompra", compra.IdCompra);
                    command.Parameters.AddWithValue("@IdUsuario", compra.usuario.idUsuario);
                    command.Parameters.AddWithValue("@IdProveedor", compra.proveedor.idProveedor);
                    command.Parameters.AddWithValue("@MontoTotal", compra.montoTotal);
                    command.ExecuteNonQuery();
                }

                //Recorremos los detalles
                foreach (var detalle in compra.detalles)
                {
                    using (SqlCommand command = new SqlCommand(registroDetalleCompra, conexion, accion))
                    {
                        command.Parameters.AddWithValue("@IdCompra", detalle.idCompra);
                        command.Parameters.AddWithValue("@IdProducto", detalle.producto.idProducto);
                        command.Parameters.AddWithValue("@PrecioCompra", detalle.precioCompra);
                        command.Parameters.AddWithValue("@PrecioVenta", detalle.precioVenta);
                        command.Parameters.AddWithValue("@CantidadProducto", detalle.cantidad);
                        command.Parameters.AddWithValue("@SubTotal", detalle.total);
                        command.ExecuteNonQuery();
                    }
                }

                accion.Commit();
            }
            catch (Exception)
            {
                if (accion != null)
                {
                    accion.Rollback();
                }
            }
            finally
            {
                CerrarConexion();
            }
        }

        public List<DetalleCompra> leerDetalle()
        {
            List<DetalleCompra> destalles = new List<DetalleCompra>();
            string consulta2 = "SELECT * FROM DETALLECOMPRA";
            try
            {
                SqlCommand command1 = new SqlCommand(consulta2, conexion);
                AbrirConexion();
                SqlDataReader reader = command1.ExecuteReader();

                while (reader.Read())
                {
                    destalles.Add(MapDetalleCompra(reader));
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception)
            {
                return null;
            }
            return destalles;
        }

        public List<Compra> LeerCompra()
        {
            List<Compra> compras = new List<Compra>();
            
            string Consulta = "SELECT * FROM COMPRA";
            try
            {
                SqlCommand command = new SqlCommand(Consulta, conexion);
                
                AbrirConexion();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    compras.Add(MapCompra(reader));
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception)
            {
                return null;
            }
            return compras;
        }
        public List<Compra> LeerCompras()
        {
            List<Compra> compras = new List<Compra>();
            if (LeerCompra() != null)
            {
                foreach (var item in LeerCompra())
                {
                    item.detalles = GetCompraList(item.IdCompra);
                    compras.Add(item);
                }
            }
            return compras;
        }

        public Compra Buscar(string IdCompra)
        {
            try
            {
                var compras = LeerCompra();
                if (compras == null)
                {
                    return null;
                }
                return compras.FirstOrDefault(p => p.IdCompra == IdCompra);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Proveedor ObtenerProveedor(string IdProveedor)
        {
            return archivoProveedor.Leer().Find(p => p.idProveedor == IdProveedor);
        }

        private Producto ObtenerProducto(string idProducto)
        {
            return archivoProducto.Leer().Find(pr => pr.idProducto == idProducto);
        }

        private Usuario ObtenerUsuario(string idUsuario)
        {
            return archivoUsuario.Leer().Find(p=>p.idUsuario == idUsuario);
        }

        public DetalleCompra MapDetalleCompra(SqlDataReader reader)
        {
            DetalleCompra detalleCompra = new DetalleCompra
            {
                idDetalleCompra = Convert.ToInt32(reader["IdDetalleCompra"]),
                precioCompra = Convert.ToDouble(reader["PrecioCompra"]),
                precioVenta = Convert.ToDouble(reader["PrecioVenta"]),
                cantidad = Convert.ToInt32(reader["CantidadProducto"]),
                total = Convert.ToDouble(reader["SubTotal"])
            };

            string idProducto = Convert.ToString(reader["IdProducto"]);
            detalleCompra.producto = ObtenerProducto(idProducto);

            detalleCompra.idCompra = Convert.ToString(reader["IdCompra"]);
            return detalleCompra;
        }

        public Compra MapCompra(SqlDataReader reader)
        {
            Compra compra = new Compra
            {
                IdCompra = Convert.ToString(reader["IdCompra"]),
                montoTotal = Convert.ToDouble(reader["MontoTotal"]),
                FechaCompra = Convert.ToDateTime(reader["FechaCompra"])
            };

            string IdProveedor = Convert.ToString(reader["IdProveedor"]);
            compra.proveedor = ObtenerProveedor(IdProveedor);
            
            string IdUsuario = Convert.ToString(reader["IdUsuario"]);
            compra.usuario = ObtenerUsuario(IdUsuario);
            return compra;
        }

        public List<DetalleCompra> GetCompraList(string idCompra)
        {
            return leerDetalle().Where(detalle => detalle.idCompra.Contains(idCompra)).ToList();
        }


        public bool Eliminar(string idCompra)
        {
            
            if(EliminarDetalle(idCompra) != false && EliminarCompra(idCompra) != false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarCompra(string IdCompra)
        {
            string query = "DELETE FROM COMPRA WHERE IdCompra = @IdCompra";

            try
            {
                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@IdCompra", IdCompra);

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

        public bool EliminarDetalle(string IdCompra)
        {
            string query = "DELETE FROM DETALLECOMPRA WHERE IdCompra = @IdCompra";

            try
            {
                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@IdCompra", IdCompra);

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

    }
}
