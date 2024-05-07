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
    public class ArchivoCompra : Icrud<Compra>
    {
        private readonly string fileName = "Compra.bin";
        FileStream archivo;
        public void Add(Compra compra)
        {
            List<Compra> compras = Leer();
            compras.Add(compra);
            Guardar(compras);
        }
        public void Guardar(List<Compra> compras)
        {
            archivo = new FileStream(fileName, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(archivo, compras);
            archivo.Close();
        }
        public List<Compra> Leer()
        {
            archivo = new FileStream(fileName, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            List<Compra> compras = (List<Compra>)formatter.Deserialize(archivo);
            archivo.Close();
            return compras;
        }
        public bool Eliminar(string IdCompra)
        {
            Compra compra = Buscar(IdCompra);
            if (compra != null )
            {
                List<Compra> compras = Remover(compra);
                Guardar(compras);
                return true;
            }
            return false;
        }
        public List<Compra> Remover(Compra compra)
        {
            List<Compra> compras = Leer();
            foreach (var item in compras)
            {
                if (item.IdCompra.Equals(compra.IdCompra))
                {
                    compras.Remove(item);
                    return compras;
                }
            }
            return compras;
        }
        public Compra Buscar(string IdCompra)
        {
            List<Compra> compras = Leer();
            foreach (var item in compras)
            {
                if (item.IdCompra.Equals(IdCompra))
                {
                    return item;
                }
            }
            return null;
        }
        public bool Actualizar(Compra compraNew)
        {
            List<Compra> compras = Leer();
            Compra compraOld = Buscar(compraNew.IdCompra);
            if (compraOld != null)
            {
                compras.Remove(compraOld);
                compras.Add(compraNew);
                Guardar(compras);
                return true;
            }
            return false;
        }
    }
}
