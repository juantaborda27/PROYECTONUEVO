using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    [Serializable]
    public class Compra
    {
        public string IdCompra { get; set; }
        public Usuario usuario { get; set; }
        public Proveedor proveedor { get; set; }
        public List<DetalleCompra> detalles { get; set; }
        public double montoTotal { get; set; }
        public DateTime FechaCompra { get; set; }
        public void calcularMonto()
        {
            foreach (var item in detalles)
            {
                montoTotal += item.total;
            }
        }
    }
}
