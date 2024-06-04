using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ArchivoCompra : BaseDatos
    {
        ArchivoProveedor archivoProveedor = new ArchivoProveedor();
        ArchivoProducto archivoProducto = new ArchivoProducto();
        ArchivoUsuario archivoUsuario = new ArchivoUsuario();
        public void Add(Compra compra, List<DetalleCompra> detallesCompra)
        {
            string registroCompra = "INSERT INTO COMPRA (IdUsuario ,IdProveedor, MontoTotal) VALUES " +
                "(@IdUsuario, @IdProveedor, @MontoTotal)";
            string registroDetalleCompra = "INSERT INTO DETALLECOMPRA (IdCompra, IdProducto,PrecioCompra, PrecioVenta,CantidadProducto, SubTotal) VALUES " +
                "(@IdCompra, @IdProducto, @PrecioCompra,@PrecioVenta, @CantidadProducto, @SubTotal)";

            SqlTransaction accion = null;

            try
            {
                AbrirConexion();
                //Una transacción es una secuencia de operaciones de base de datos que se ejecutan de forma atómica. 
                accion = conexion.BeginTransaction();
                using (SqlCommand command = new SqlCommand(registroCompra, conexion, accion))
                {
                    //command.Parameters.AddWithValue("@IdCompra", compra.IdCompra);
                    command.Parameters.AddWithValue("@IdUsuario", 1);
                    command.Parameters.AddWithValue("@IdProveedor", compra.proveedor);
                    command.Parameters.AddWithValue("@MontoTotal", compra.montoTotal);
                    command.ExecuteNonQuery();
                }

                //Recorremos los detalles
                foreach (var detalle in detallesCompra)
                {
                    using (SqlCommand command = new SqlCommand(registroDetalleCompra, conexion, accion))
                    {
                        command.Parameters.AddWithValue("@IdCompra", compra.IdCompra);
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

        public List<Compra> Leer()
        {
            List<Compra> compras = new List<Compra>();
            List<DetalleCompra> destalles = new List<DetalleCompra>();
            string Consulta = "SELECT * FROM COMPRA";
            string consulta2 = "SELECT * FROM DETALLECOMPRA";
            try
            {
                SqlCommand command = new SqlCommand(Consulta, conexion);
                SqlCommand command1 = new SqlCommand(consulta2,conexion);
                AbrirConexion();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    compras.Add(MapCompra(reader));
                }
                

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
            return compras;
        }

        public List<DetalleCompra> LeerDetalleCompra(string IdCompra)
        {
            List<DetalleCompra> detalleCompraList = new List<DetalleCompra>();
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("SELECT p.DescripcionProducto,dc.PrecioCompra,dc.CantidadProducto,dc.SubTotal");
            stringBuilder.AppendLine("FROM DETALLECOMPRA dc");
            stringBuilder.AppendLine("inner join PRODUCTO p ON p.IdProducto = dc.IdProducto");
            stringBuilder.AppendLine("WHERE dc.IdCompra = @IdCompra");
            try
            {
                SqlCommand command = new SqlCommand(stringBuilder.ToString(), conexion);
                command.Parameters.AddWithValue("@IdCompra", IdCompra);
                command.CommandType = CommandType.Text;
                AbrirConexion();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    detalleCompraList.Add(MapDetalleCompra(reader));
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception)
            {
                return null;
            }
            return detalleCompraList;
        }

        public Compra Buscar(string IdCompra)
        {
            Compra compra = new Compra();
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("SELECT c.IdCompra, p.IdProveedor,");
            stringBuilder.AppendLine("c.MontoTotal FROM COMPRA c");
            stringBuilder.AppendLine("inner join PROVEEDOR p ON p.IdProveedor = c.IdProveedor");
            stringBuilder.AppendLine("WHERE c.IdCompra = @IdCompra");
            try
            {
                SqlCommand command = new SqlCommand(stringBuilder.ToString(), conexion);
                command.Parameters.AddWithValue("@IdCompra", IdCompra);
                command.CommandType = CommandType.Text;
                AbrirConexion();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    compra = MapCompra(reader);
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception)
            {
                return null;
            }
            return compra;
        }



        private Proveedor ObtenerProveedor(string IdProveedor)
        {
            return archivoProveedor.Leer().Find(p => p.idProveedor == IdProveedor);
        }

        private Producto ObtenerProducto(string idProducto)
        {
            return archivoProducto.Leer().Find(pr => pr.idProducto == idProducto);
        }

        private Usuario ObtenerUsuario(string cedula)
        {
            return archivoUsuario.Leer().Find(p=>p.cedula == cedula);
        }

        private Compra ObtenerCompra(string idCompra)
        {
            return Leer().Find(p=>p.IdCompra == idCompra);
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

            string idCompra = Convert.ToString(reader["IdProducto"]);
            detalleCompra.compra = ObtenerCompra(idCompra);
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
    }
}
