using ENTIDADES;
using Logica;
using System.Windows;
using System.Windows.Controls;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ReportesCompra.xaml
    /// </summary>
    public partial class ReportesCompra : UserControl
    {
        LogicaCompra logicaCompra = new LogicaCompra();
        public ReportesCompra()
        {
            InitializeComponent();
            tbCompra.DataContext = logicaCompra.Leer();
        }

        private void BtnDetalle_Click(object sender, RoutedEventArgs e)
        {
            if (tbCompra.SelectedItem != null)
            {
                var compra = (Compra)tbCompra.SelectedItem;
                DetalleCompraVista vista = new DetalleCompraVista(logicaCompra.Buscar(compra.IdCompra).detalles);
                vista.Show();
            }
            else
            {
                MessageBox.Show("Seleccione una factura", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (tbCompra.SelectedItems != null)
            {
                var compra = (Compra)tbCompra.SelectedItem;
                MessageBoxResult result = MessageBox.Show("¿Estás seguro que deseas continuar?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    //MessageBox.Show(logicaCompra.Eliminar(compra.IdCompra));
                    ActualizarTabla();
                }
            }
            else
            {
                MessageBox.Show("Seleccione una factura", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        void ActualizarTabla()
        {
            tbCompra.DataContext = null;
            tbCompra.DataContext = logicaCompra.Leer();
        }
    }
}
