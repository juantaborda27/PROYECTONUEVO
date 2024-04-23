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
        public double montoTotal { get; set; }
        public string FechaCompra { get; set; }
    }
}
