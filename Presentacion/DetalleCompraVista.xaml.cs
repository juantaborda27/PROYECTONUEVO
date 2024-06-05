using ENTIDADES;
using Logica;
using System.Collections.Generic;
using System.Windows;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para DetalleCompraVista.xaml
    /// </summary>
    public partial class DetalleCompraVista : Window
    {
        LogicaCompra logicaCompra = new LogicaCompra();
        public DetalleCompraVista(string detalles)
        {

            InitializeComponent();
            tbDetalleCompra.DataContext = logicaCompra.LeerDetalleCompra(detalles);
        }
    }
}
