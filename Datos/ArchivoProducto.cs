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
            archivo = new FileStream(fileName, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            List<Producto> productos = (List<Producto>)formatter.Deserialize(archivo);
            archivo.Close();
            return productos;
        }
        public bool Eliminar(string idProducto)
        {
            List<Producto> productos = Leer();
            Producto producto = Buscar(idProducto);

            if (producto != null && productos != null)
            {
                productos.Remove(producto);
                Guardar(productos);
                return true;
            }
            return false;
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
                productos.Remove(productoOld);
                productos.Add(productoNew);
                Guardar(productos);
                return true;
            }
            return false;
        } 
    }
}
