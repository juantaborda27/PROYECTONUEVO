using System;


namespace ENTIDADES
{
    [Serializable]
    public class Proveedor
    {
        public string idProveedor { get; set; }
        public string documento { get; set; }
        public string telefono { get; set; }
        public string NombreProveedor { get; set; }
    }
}
