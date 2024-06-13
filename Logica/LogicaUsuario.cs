using Datos;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class LogicaUsuario
    {
        ArchivoUsuario archivoUsuario = new ArchivoUsuario();
        public string Add(Usuario usuario)
        {
            archivoUsuario.Add(usuario);
            return "";
        }
        public List<Usuario> Leer()
        {
            return archivoUsuario.Leer();
        }
        public string Eliminar(string Cedula)
        {
            if (archivoUsuario.Eliminar(Cedula))
            {
                return "Se ha eliminado correctamente";
            }
            else
            {
                return "No se puede eliminar al usuario, tiene elementos asociados";
            }
        }
        public Usuario Buscar(string idUsuario)
        {
            return archivoUsuario.Buscar(idUsuario);
        }
        public string Actualizar(Usuario usuario)
        {
            if (archivoUsuario.Actualizar(usuario))
            {
                return "Se ha modificado correctamente";
            }
            else
            {
                return "Producto no identificado";
            }
        }
    }
}
