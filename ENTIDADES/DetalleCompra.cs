using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    [Serializable]
    public class DetalleCompra
    {
        public int idDetalleCompra { get; set; }
        public Producto producto { get; set; }
        public double precioCompra { get; set; }
        public double precioVenta { get; set; }
        public int cantidad { get; set; }
        public double total { get; set; }
    }
}
