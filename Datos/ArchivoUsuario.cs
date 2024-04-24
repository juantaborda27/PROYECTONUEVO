using ENTIDADES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Datos
{
    public class ArchivoUsuario:Icrud<Usuario>
    {
        private readonly string fileName = "Usuarios.bin";

        public void Add(Usuario usuario)
        {
            List<Usuario> usuarios = Leer();
            usuarios.Add(usuario);
            Guardar(usuarios);
        }

        public void Guardar(List<Usuario> usuarios)
        {
            using (FileStream archivo = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(archivo, usuarios);
            }
        }

        public List<Usuario> Leer()
        {
            if (!File.Exists(fileName))
            {
                return new List<Usuario>();
            }

            using (FileStream archivo = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<Usuario>)formatter.Deserialize(archivo);
            }
        }

        public bool Eliminar(string cedula)
        {
            List<Usuario> usuarios = Leer();
            Usuario usuario = Buscar(cedula);

            if (usuario != null && usuarios != null)
            {
                usuarios.Remove(usuario);
                Guardar(usuarios);
                return true;
            }

            return false;
        }

        public Usuario Buscar(string cedula)
        {
            List<Usuario> usuarios = Leer();
            foreach (var item in usuarios)
            {
                if (item.cedula.Equals(cedula))
                {
                    return item;
                }
            }
            return null;
        }

        public bool Actualizar(Usuario usuarioNew)
        {
            List<Usuario> usuarios = Leer();
            Usuario usuarioOld = Buscar(usuarioNew.cedula);

            if (usuarioOld != null)
            {
                usuarios.Remove(usuarioOld);
                usuarios.Add(usuarioNew);
                Guardar(usuarios);
                return true;
            }

            return false;
        }
    }
}
