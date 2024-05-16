using ENTIDADES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ArchivoProducto:Icrud<Producto>
    {
        private readonly string fileName = "Producto.bin";
        FileStream archivo;
        public void Add(Producto producto)
        {
            List<Producto> productos = Leer();
            productos.Add(producto);
            Guardar(productos);
        }
        public void Guardar(List<Producto> productos)
        {
            archivo = new FileStream(fileName, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(archivo, productos);
            archivo.Close();
        }

        public List<Producto> Leer()
        {
            if (!File.Exists(fileName))
            {
                return new List<Producto>();
            }

            using (FileStream archivo = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<Producto>)formatter.Deserialize(archivo);
            }
        }

        public bool Eliminar(string idProducto)
        {

            Producto producto = Buscar(idProducto);

            if (producto != null)
            {
                //categoriaProductos.Remove(categoriaProducto);
                List<Producto> productos = Remover(producto);
                Guardar(productos);
                return true;
            }

            return false;
        }
        public List<Producto> Remover(Producto producto)
        {
            List<Producto> productos = Leer();
            foreach (var item in productos)
            {
                if (item.idProducto.Equals(producto.idProducto))
                {
                    productos.Remove(item);
                    return productos;
                }
            }
            return productos;
        }
        public Producto Buscar(string idProducto)
        {
            List<Producto> productos = Leer();
            foreach (var item in productos)
            {
                if (item.idProducto.Equals(idProducto))
                {
                    return item;
                }
            }
            return null;
        }
        public bool Actualizar(Producto productoNew)
        {
            List<Producto> productos = Leer();
            Producto productoOld = Buscar(productoNew.idProducto);
            if (productoOld != null)
            {
                Eliminar(productoOld.idProducto);
                Add(productoNew);
                return true;
            }
            return false;
        } 
    }
}
