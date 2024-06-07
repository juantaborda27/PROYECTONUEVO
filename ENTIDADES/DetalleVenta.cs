using System;


namespace ENTIDADES
{
    [Serializable]
    public class DetalleVenta
    {
        public int idDetalleVenta { get; set; }
        public int idVenta { get; set; }
        public Producto producto { get; set; }
        public double precioVenta { get; set; }
        public int cantidad { get; set; }
        public double total { get; set; }
        public void CalcularTotal()
        {
            total = cantidad*precioVenta;
        }
    }
}
