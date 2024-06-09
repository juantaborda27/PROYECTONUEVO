using ENTIDADES;
using Logica;
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
        public DetalleVentaVista(string idCompra)
        {
            InitializeComponent();
            tblVistaVenta.DataContext = logicaVenta.LeerDetalleCompra(idCompra);
        }

    }
}
