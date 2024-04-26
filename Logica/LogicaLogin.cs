using Datos;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class LogicaLogin
    {
        ArchivoUsuario data;
        public LogicaLogin()
        {
            data = new ArchivoUsuario();
            //Admin();
        }
        public Usuario Loguear(Usuario loguear)
        {
            List<Usuario> usuarios = data.Leer();
            foreach (var item in usuarios)
            {
                if (item.userName.Equals(loguear.userName) && (item.contraseña.Equals(loguear.contraseña)))
                {
                    return item;
                }
            }
            return null;
        }
        //public void Admin()
        //{
        //    Usuario administrador = new Usuario();
        //    administrador.userName = "Admin";
        //    administrador.contraseña = "1234";
        //    data.AddUsuario(administrador);
        //}

    }
}
