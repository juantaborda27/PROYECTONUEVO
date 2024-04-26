using Datos;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class LogicaProducto
    {
        ArchivoProducto ArchivoProducto = new ArchivoProducto();

        public string Add(Producto producto)
        {
            ArchivoProducto.Add(producto);
            return "";
        }
        public List<Producto> Leer()
        {
            return ArchivoProducto.Leer();
        }
        public string Eliminar(string idProducto)
        {
            if (ArchivoProducto.Eliminar(idProducto))
            {
                return "Se ha eliminado de correctamente";
            }
            else
            {
                return "No existe el id ingresado";
            }
        }
        public Producto Buscar(string idProducto)
        {
            return ArchivoProducto.Buscar(idProducto);
        }
        public string Actualizar(Producto producto)
        {
            if (ArchivoProducto.Actualizar(producto))
            {
                return "Se ha modificado correctamente";
            }
            else
            {
                return "Producto no identificado";
            }
        }
    }
}
