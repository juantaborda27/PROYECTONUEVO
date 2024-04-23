using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    [Serializable]
    public class Proveedor
    {
        public string idProveedor { get; set; }
        public string documento { get; set; }
        public string telefono { get; set; }
        public string fechaCreacion { get; set; }
    }
}
