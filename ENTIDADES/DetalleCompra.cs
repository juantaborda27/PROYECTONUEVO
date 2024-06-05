using System;


namespace ENTIDADES
{
    [Serializable]
    public class DetalleCompra
    {
        public int idDetalleCompra { get; set; }
        public Producto producto { get; set; }
        
        public string idCompra {  get; set; }
        public double precioCompra { get; set; }
        public double precioVenta { get; set; }
        public int cantidad { get; set; }
        public double total { get; set; }
        public void CalcularTotal()
        {
            total = cantidad * precioCompra;
        }
    }
    
}
