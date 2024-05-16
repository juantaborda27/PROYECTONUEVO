using Datos;
using ENTIDADES;
using System.Collections.Generic;


namespace Logica
{
    public class LogicaCategoria
    {
        ArchivoCategoria data = new ArchivoCategoria();
        public string Add(CategoriaProducto categoriaProducto)
        {
            data.Add(categoriaProducto);
            return "";
        }
        public List<CategoriaProducto> Leer()
        {
            return data.Leer();
        }
        public string Eliminar(string descripcion)
        {
            if (data.Eliminar(descripcion))
            {
                return "Se ha eliminado de correctamente";
            }
            else
            {
                return "No existe el id ingresado";
            }
        }
        public CategoriaProducto Buscar(string descripcion)
        {
            return data.Buscar(descripcion);
        }
        public string Actualizar(CategoriaProducto categoriaProducto)
        {
            if (data.Actualizar(categoriaProducto))
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
