using System;
using System.Collections.Generic;
namespace ENTIDADES
{
    [Serializable]
    public class Venta
    {
        public string idVenta { get; set; }
        public Usuario usuario { get; set; }
        public List<DetalleVenta> detalles { get; set; }
        public double montoPago { get; set; }
        public double montoCambio { get; set; }
        public double montoTotal { get; set; }
        public DateTime FechaVenta { get; set; }
        public Cliente cliente { get; set; }
        public void CalcularTotal()
        {
            foreach (var item in detalles)
            {
                montoTotal += item.total;
            }
        }
        public void CalcularCambio()
        {
            montoCambio = montoPago - montoTotal;
        }

    }
}
