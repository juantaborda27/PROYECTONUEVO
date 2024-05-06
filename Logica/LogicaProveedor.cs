using Datos;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    
    public class LogicaProveedor
    {
        ArchivoProveedor archivoProveedor = new ArchivoProveedor();
        public string Add(Proveedor proveedor)
        {
            archivoProveedor.Add(proveedor);
            return "";
        }
        public List<Proveedor> Leer()
        {
            return archivoProveedor.Leer();
        }
        public string Eliminar(string documento)
        {
            if (archivoProveedor.Eliminar(documento))
            {
                return "Se ha eliminado correctamente";
            }
            else
            {
                return "No existe el id ingresado";
            }
        }
        public Proveedor Buscar(string documento)
        {
            return archivoProveedor.Buscar(documento);
        }
        public string Actualizar(Proveedor proveedor)
        {
            if (archivoProveedor.Actualizar(proveedor))
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
