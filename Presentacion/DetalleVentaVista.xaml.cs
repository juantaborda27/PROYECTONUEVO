using ENTIDADES;
using System.Collections.Generic;
using System.Windows;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para DetalleVentaVista.xaml
    /// </summary>
    public partial class DetalleVentaVista : Window
    {
        public DetalleVentaVista(List<DetalleVenta> detalleVentas)
        {
            InitializeComponent();
            tblVistaVenta.DataContext = detalleVentas;
        }

    }
}
