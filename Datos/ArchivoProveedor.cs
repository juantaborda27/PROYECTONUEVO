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
            archivo = new FileStream(fileName, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            List<Proveedor> proveedores = (List<Proveedor>)formatter.Deserialize(archivo);
            archivo.Close();
            return proveedores;
        }
        public bool Eliminar(string idProveedor)
        {
            List<Proveedor> proveedores = Leer();
            Proveedor proveedor = Buscar(idProveedor);

            if (proveedor != null && proveedores != null)
            {
                proveedores.Remove(proveedor);
                Guardar(proveedores);
                return true;
            }
            return false;
        }
        public Proveedor Buscar(string idProveedor)
        {
            List<Proveedor> proveedores = Leer();
            foreach (var item in proveedores)
            {
                if (item.idProveedor.Equals(idProveedor))
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
