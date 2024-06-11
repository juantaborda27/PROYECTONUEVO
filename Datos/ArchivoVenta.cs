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
    public class ArchivoVenta: BaseDatos
    {
        ArchivoUsuario archivoUsuario = new ArchivoUsuario();
        ArchivoCliente archivoCliente = new ArchivoCliente();
        ArchivoProducto archivoProducto = new ArchivoProducto();
        public void Add(Venta venta)
        {
            string registroVenta = "INSERT INTO VENTA (IdVenta,IdUsuario,DocumentoCliente,MontoPago,MontoCambio,MontoTotal) VALUES " +
                "(@IdVenta,@IdUsuario,@DocumentoCliente,@MontoPago,@MontoCambio,@MontoTotal)";
            string registroDetalleVenta = "INSERT INTO DETALLEVENTA (IdVenta,IdProducto,PrecioVenta,Cantidad,SubTotal) " +
                "VALUES" + "(@IdVenta, @IdProducto, @PrecioVenta,@Cantidad,@SubTotal)";

            SqlTransaction accion = null;

            try
            {
                AbrirConexion();
                //Una transacción es una secuencia de operaciones de base de datos que se ejecutan de forma atómica. 
                accion = conexion.BeginTransaction();
                using (SqlCommand command = new SqlCommand(registroVenta, conexion, accion))
                {
                    command.Parameters.AddWithValue("@IdVenta", venta.idVenta);
                    command.Parameters.AddWithValue("@IdUsuario", venta.usuario.idUsuario);
                    command.Parameters.AddWithValue("@DocumentoCliente", venta.cliente.Documento);
                    command.Parameters.AddWithValue("@MontoPago", venta.montoPago);
                    command.Parameters.AddWithValue("@MontoCambio", venta.montoCambio);
                    command.Parameters.AddWithValue("@MontoTotal", venta.montoTotal);
                    command.ExecuteNonQuery();
                }

                //Recorremos los detalles
                foreach (var detalle in venta.detalles)
                {
                    using (SqlCommand command = new SqlCommand(registroDetalleVenta, conexion, accion))
                    {
                        command.Parameters.AddWithValue("@IdVenta", detalle.idVenta);
                        command.Parameters.AddWithValue("@IdProducto", detalle.producto.idProducto);
                        command.Parameters.AddWithValue("@PrecioVenta", detalle.precioVenta);
                        command.Parameters.AddWithValue("@Cantidad", detalle.cantidad);
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
        
        public List<Venta> Leer()
        {
            List<Venta> ventas = new List<Venta>();

            string Consulta = "SELECT * FROM VENTA";
            try
            {
                SqlCommand command = new SqlCommand(Consulta, conexion);

                AbrirConexion();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ventas.Add(MapVenta(reader));
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception)
            {
                return null;
            }
            return ventas;
        }

        public List<DetalleVenta> leerDetalleVenta()
        {
            List<DetalleVenta> detalles = new List<DetalleVenta>();
            string consulta2 = "SELECT * FROM DETALLEVENTA";
            try
            {
                SqlCommand command1 = new SqlCommand(consulta2, conexion);
                AbrirConexion();
                SqlDataReader reader = command1.ExecuteReader();

                while (reader.Read())
                {
                    detalles.Add(MapDetalleVenta(reader));
                }
                reader.Close();
                CerrarConexion();
            }
            catch (Exception)
            {
                return null;
            }
            return detalles;
        }

        public bool Eliminar(string idVenta)
        {
            if (EliminarDetalleVenta(idVenta) != false && EliminarVenta(idVenta) != false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarVenta(string idVenta)
        {
            string query = "DELETE FROM VENTA WHERE IdVenta = @IdVenta";

            try
            {
                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@IdVenta", idVenta);

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

        public bool EliminarDetalleVenta(string idVenta)
        {
            string query = "DELETE FROM DETALLEVENTA WHERE IdVenta = @IdVenta";
            try
            {
                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@IdVenta", idVenta);

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
        public Venta Buscar(string idVenta)
        {
            try
            {
                var ventas = Leer();
                if (ventas == null)
                {
                    return null;
                }
                return ventas.FirstOrDefault(p => p.idVenta == idVenta);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //public bool Actualizar(Venta ventaNew)
        //{
        //    List<Venta> ventas = Leer();
        //    Venta ventaOld = Buscar(ventaNew.idVenta);

        //    if (ventaOld != null)
        //    {
        //        ventas.Remove(ventaOld);
        //        ventas.Add(ventaNew);
        //        Guardar(ventas);
        //        return true;
        //    }

        //    return false;
        //}

        public Venta MapVenta(SqlDataReader reader)
        {
            Venta venta = new Venta
            {
                idVenta = Convert.ToString(reader["IdVenta"]),
                montoPago = Convert.ToDouble(reader["MontoPago"]),
                montoCambio = Convert.ToDouble(reader["MontoCambio"]),
                montoTotal = Convert.ToDouble(reader["MontoTotal"]),
                FechaVenta = Convert.ToDateTime(reader["FechaVenta"])
            };

            string IdUsuario = Convert.ToString(reader["IdUsuario"]);
            venta.usuario = ObtenerUsuario(IdUsuario);

            string Documento = Convert.ToString(reader["DocumentoCliente"]);
            venta.cliente = ObtenerCliente(Documento);
            return venta;
        }

        public DetalleVenta MapDetalleVenta(SqlDataReader reader)
        {
            DetalleVenta detalleVenta = new DetalleVenta
            {
                idDetalleVenta = Convert.ToInt32(reader["IdDetalleVenta"]),
                idVenta = Convert.ToInt32(reader["IdVenta"]),
                precioVenta = Convert.ToDouble(reader["PrecioVenta"]),
                cantidad = Convert.ToInt32(reader["Cantidad"]),
                total = Convert.ToDouble(reader["SubTotal"])
            };

            string idProducto = Convert.ToString(reader["IdProducto"]);
            detalleVenta.producto = ObtenerProducto(idProducto);

            return detalleVenta;
        }

        private Producto ObtenerProducto(string idProducto)
        {
            return archivoProducto.Leer().Find(pr => pr.idProducto == idProducto);
        }

        private Usuario ObtenerUsuario(string cedula)
        {
            return archivoUsuario.Leer().Find(p => p.cedula == cedula);
        }

        private Cliente ObtenerCliente(string Documento)
        {
            return archivoCliente.Leer().Find(p => p.Documento == Documento);
        }

        public List<DetalleVenta> GetVentaList(string idVenta)
        {
            return leerDetalleVenta().Where(detalle => detalle.idVenta.Equals(idVenta)).ToList();
        }

    }
}
