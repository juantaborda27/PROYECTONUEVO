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
        public string Eliminar(int IdCategoria)
        {
            if (data.Eliminar(IdCategoria))
            {
                return "Se ha eliminado de correctamente";
            }
            else
            {
                return "No existe el id ingresado";
            }
        }
        public CategoriaProducto Buscar(int IdCategoria)
        {
            return data.Buscar(IdCategoria);
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
