using Datos;
using ENTIDADES;
using System.Collections.Generic;

namespace Logica
{
    public class LogicaVenta
    {
        ArchivoVenta archivoVenta =new ArchivoVenta();
        LogicaProducto logicaProducto = new LogicaProducto();

        public string Add(Venta venta)
        {
            archivoVenta.Add(venta);
            Salida(venta);
            return "Se registro la venta correctamente";
        }
        public Venta Buscar(string idVenta)
        {
            return archivoVenta.Buscar(idVenta);
        }
        public List<Venta> Leer()
        {
            return archivoVenta.Leer();
        }
        public string Eliminar(string idVenta)
        {
            if (archivoVenta.Eliminar(idVenta)){
                return "Se elimino correctamente";
            }
            else
            {
                return "No existe dicha venta";
            }
        }

        public List<DetalleVenta> LeerDetalleCompra(string idVenta)
        {
            return archivoVenta.GetVentaList(idVenta);
        }
        public void Salida(Venta venta)
        {
            foreach (var item in venta.detalles)
            {
                item.producto.cantidad -= item.cantidad;
                logicaProducto.Actualizar(item.producto);
            }
        }
    }
}

