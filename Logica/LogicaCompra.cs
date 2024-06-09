using Datos;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data;


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
            return datos.LeerCompra();
        }
        public List<DetalleCompra> LeerDetalleCompra(string idCompra)
        {
            return datos.GetCompraList(idCompra);
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
            foreach (var item in compra.detalles)
            {
                item.producto.precioCompra=item.precioCompra;
                item.producto.precioVenta = item.precioVenta;
                item.producto.cantidad += item.cantidad;
                logicaProducto.Actualizar(item.producto);
            }
        }
    }
}
