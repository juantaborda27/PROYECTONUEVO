using System;
namespace ENTIDADES
{
    [Serializable]
    public class Usuario
    {
        public string idUsuario { get; set; }
        public string userName { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string contraseña { get; set; }
        public string rol { get; set; }
    }
}
