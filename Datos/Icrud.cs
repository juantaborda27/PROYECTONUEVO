using ENTIDADES;
using System;
using System.Collections.Generic;


namespace Datos
{
    public interface Icrud<T>
    {
        void Add(T entidad);
        void Guardar(List<T> entidades);
        List<T> Leer();
        bool Eliminar(string identificacion);
        T Buscar(string identificacion);
        bool Actualizar(T entidad);
    }
}
