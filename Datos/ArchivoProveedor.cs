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
    public class ArchivoProveedor:Icrud<Proveedor>
    {
        private readonly string fileName = "Proveedor.bin";
        FileStream archivo;

        public void Add(Proveedor proveedor)
        {
            List<Proveedor> proveedores = Leer();
            proveedores.Add(proveedor);
            Guardar(proveedores);
        }
        public void Guardar(List<Proveedor> proveedores)
        {
            archivo = new FileStream(fileName, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(archivo, proveedores);
            archivo.Close();
        }
        public List<Proveedor> Leer()
        {
            if (!File.Exists(fileName))
            {
                return new List<Proveedor>();
            }

            using (FileStream archivo = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<Proveedor>)formatter.Deserialize(archivo);
            }
        }

        public bool Eliminar(string documento)
        {

            Proveedor proveedor = Buscar(documento);

            if (proveedor != null)
            {
                //categoriaProductos.Remove(categoriaProducto);
                List<Proveedor> proveedores = Remover(proveedor);
                Guardar(proveedores);
                return true;
            }

            return false;
        }
        public List<Proveedor> Remover(Proveedor proveedor)
        {
            List<Proveedor> proveedores = Leer();
            foreach (var item in proveedores)
            {
                if (item.documento.Equals(proveedor.documento))
                {
                    proveedores.Remove(item);
                    return proveedores;
                }
            }
            return proveedores;
        }
        public Proveedor Buscar(string documento)
        {
            List<Proveedor> proveedores = Leer();
            foreach (var item in proveedores)
            {
                if (item.documento.Equals(documento))
                {
                    return item;
                }
            }
            return null;
        }
        public bool Actualizar(Proveedor proveedorNew)
        {
            List<Proveedor> proveedores = Leer();
            Proveedor proveedorOld = Buscar(proveedorNew.idProveedor);
            if (proveedorOld != null)
            {
                proveedores.Remove(proveedorOld);
                proveedores.Add(proveedorNew);
                Guardar(proveedores);
                return true;
            }
            return false;
        }
    }
}
