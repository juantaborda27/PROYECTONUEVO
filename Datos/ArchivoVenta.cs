using ENTIDADES;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Datos
{
    public class ArchivoVenta:Icrud<Venta> 
    {
        private readonly string fileName = "Factura.bin";
        FileStream archivo;

        public void Add(Venta venta)
        {
            List<Venta> ventas = Leer();
            ventas.Add(venta);
            Guardar(ventas);
        }
        public void Guardar(List<Venta> ventas)
        {
            using (FileStream archivo = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(archivo, ventas);
            }
        }
        public List<Venta> Leer()
        {
            if (!File.Exists(fileName))
            {
                return new List<Venta>();
            }

            using (FileStream archivo = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<Venta>)formatter.Deserialize(archivo);
            }
        }
        public bool Eliminar(string idVenta)
        {
            List<Venta> ventas = Leer();
            Venta venta = Buscar(idVenta);

            if (ventas != null && venta != null)
            {
                ventas.Remove(venta);
                Guardar(ventas);
                return true;
            }

            return false;
        }
        public Venta Buscar(string idVenta)
        {
            List<Venta> ventas = Leer();
            foreach (var item in ventas)
            {
                if (item.idVenta.Equals(idVenta))
                {
                    return item;
                }
            }
            return null;
        }
        public bool Actualizar(Venta ventaNew)
        {
            List<Venta> ventas = Leer();
            Venta ventaOld = Buscar(ventaNew.idVenta);

            if (ventaOld != null)
            {
                ventas.Remove(ventaOld);
                ventas.Add(ventaNew);
                Guardar(ventas);
                return true;
            }

            return false;
        }
    }
}
