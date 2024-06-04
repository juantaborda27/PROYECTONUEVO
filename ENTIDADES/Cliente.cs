using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Documento { get; set; }
        public string NombreCliente { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
    }
}
