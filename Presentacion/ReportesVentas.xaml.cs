using ENTIDADES;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ReportesVentas.xaml
    /// </summary>
    public partial class ReportesVentas : UserControl
    {
        LogicaVenta logicaVenta = new LogicaVenta();
        public ReportesVentas()
        {
            InitializeComponent();
            tbVenta.DataContext = logicaVenta.Leer();
        }

        private void BtnDetalle_Click(object sender, RoutedEventArgs e)
        {
            if (tbVenta.SelectedItem != null)
            {
                var venta = (Venta)tbVenta.SelectedItem;
                DetalleVentaVista ventana = new DetalleVentaVista(venta.detalles);
                ventana.Show();
            }
            else
            {
                MessageBox.Show("Seleccione una factura", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (tbVenta.SelectedItem != null)
            {
                var venta = (Venta)tbVenta.SelectedItem;
                MessageBoxResult result = MessageBox.Show("¿Estás seguro que deseas continuar?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show(logicaVenta.Eliminar(venta.idVenta));
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
            tbVenta.DataContext = null;
            tbVenta.DataContext = logicaVenta.Leer();

        }
    }
}
