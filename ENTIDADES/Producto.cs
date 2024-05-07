using System;


namespace ENTIDADES
{
    [Serializable]
    public class Producto
    {
        public string idProducto { get; set; }
        public string descripcion { get; set; }
        public CategoriaProducto categoriaProducto { get; set; }
        public int cantidad { get; set; }
        public double precioCompra { get; set; }
        public double precioVenta { get; set; }
        public int cantidadMinima { get; set; }
    }
}
