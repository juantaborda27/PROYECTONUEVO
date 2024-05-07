using Datos;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class LogicaCompra
    {
        ArchivoCompra datos = new ArchivoCompra();
        LogicaProducto logicaProducto = new LogicaProducto();

        public string Add(Compra compra)
        {
            datos.Add(compra);
            Entrada(compra);
            return "Se registro la venta correctamente";
        }
        public Compra Buscar(string idCompra)
        {
            return datos.Buscar(idCompra);
        }
        public List<Compra> Leer()
        {
            return datos.Leer();
        }
        public string Eliminar(string idCompra)
        {
            if (datos.Eliminar(idCompra))
            {
                return "Se elimino correctamente";
            }
            else
            {
                return "No existe dicha venta";
            }
        }
        public void Entrada(Compra compra)
        {
            List<DetalleCompra> detalleCompras = compra.detalles;
            foreach (var item in detalleCompras)
            {
                Producto producto = item.producto;
                producto.cantidad = producto.cantidad - item.cantidad;
                logicaProducto.Actualizar(producto);
            }
        }
    }
}
