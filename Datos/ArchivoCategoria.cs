using ENTIDADES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Datos
{
    public class ArchivoCategoria:Icrud<CategoriaProducto>
    {
        private readonly string fileName = "Categoria.bin";

        public void Add(CategoriaProducto categoriaProducto)
        {
            List<CategoriaProducto> categoriaProductos = Leer();
            categoriaProductos.Add(categoriaProducto);
            Guardar(categoriaProductos);
        }

        public void Guardar(List<CategoriaProducto> categoriaProductos)
        {
            using (FileStream archivo = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(archivo, categoriaProductos);
            }
        }

        public List<CategoriaProducto> Leer()
        {
            if (!File.Exists(fileName))
            {
                return new List<CategoriaProducto>();
            }

            using (FileStream archivo = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<CategoriaProducto>)formatter.Deserialize(archivo);
            }
        }

        public bool Eliminar(string descripcion)
        {
            List<CategoriaProducto> categoriaProductos = Leer();
            CategoriaProducto categoriaProducto = Buscar(descripcion);

            if (categoriaProducto != null)
            {
                categoriaProductos.Remove(categoriaProducto);
                Guardar(categoriaProductos);
                return true;
            }

            return false;
        }

        public CategoriaProducto Buscar(string descripcion)
        {
            List<CategoriaProducto> categoriaProductos = Leer();
            foreach (var item in categoriaProductos)
            {
                if (item.descripcion.Equals(descripcion))
                {
                    return item;
                }
            }
            return null;
        }

        public bool Actualizar(CategoriaProducto categoriaProductoNew)
        {
            List<CategoriaProducto> categoriaProductos = Leer();
            CategoriaProducto categoriaProductoOld = Buscar(categoriaProductoNew.descripcion);

            if (categoriaProductoOld != null)
            {
                categoriaProductos.Remove(categoriaProductoOld);
                categoriaProductos.Add(categoriaProductoNew);
                Guardar(categoriaProductos);
                return true;
            }

            return false;
        }
    }
}
