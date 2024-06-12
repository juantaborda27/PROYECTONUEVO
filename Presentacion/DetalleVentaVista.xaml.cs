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
        public DetalleVentaVista(List<DetalleVenta> ventas)
        {

            InitializeComponent();
            tbVistaVenta.DataContext = ventas;
        }

    }
}
