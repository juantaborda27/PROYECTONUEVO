using ENTIDADES;
using System.Collections.Generic;
using System.Windows;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para DetalleCompraVista.xaml
    /// </summary>
    public partial class DetalleCompraVista : Window
    {
        public DetalleCompraVista(List<DetalleCompra> detalles)
        {

            InitializeComponent();
            tbDetalleCompra.DataContext = detalles;
        }
    }
}
