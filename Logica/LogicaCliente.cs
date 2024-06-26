﻿using Datos;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class LogicaCliente
    {
        ArchivoCliente archivoCliente = new ArchivoCliente();
        public string Add(Cliente cliente)
        {
            archivoCliente.Add(cliente);
            return "";
        }
        public List<Cliente> Leer()
        {
            return archivoCliente.Leer();
        }
        public string Eliminar(string Documento)
        {
            if (archivoCliente.Eliminar(Documento))
            {
                return "Se ha eliminado correctamente";
            }
            else
            {
                return "No se pudo Eliminar, tiene elementos asociados";
            }
        }
        public Cliente Buscar(string documento)
        {
            return archivoCliente.Buscar(documento);
        }
        
    }
}
