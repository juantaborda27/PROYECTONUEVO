using ENTIDADES;
using Logica;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para DetalleVentaVista.xaml
    /// </summary>
    public partial class DetalleVentaVista : Window
    {
        LogicaVenta logicaVenta = new LogicaVenta();
        public DetalleVentaVista(string idVenta)
        {

            InitializeComponent();
            List<DetalleVenta> detalles = logicaVenta.LeerDetalleVenta(idVenta);
            tbVistaVenta.DataContext = detalles;
        }

    }
}
