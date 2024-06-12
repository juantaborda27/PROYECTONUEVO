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
            List<Venta> ventas = logicaVenta.Leer();
            tablaVenta.DataContext= ventas;
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (tablaVenta.SelectedItem != null)
            {
                var venta = (Venta)tablaVenta.SelectedItem;
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
            tablaVenta.DataContext = null;
            tablaVenta.DataContext = logicaVenta.Leer();

        }

        private void BtnBuscarVenta_Click(object sender, RoutedEventArgs e)
        {
            if (idBuscarRventa.Text.Equals("") || idBuscarRventa.Text == null)
            {
                ActualizarTabla();
                return;
            }
            string buscar = idBuscarRventa.Text;
            Venta buscado = logicaVenta.Buscar(buscar);

            if (buscado != null)
            {
                tablaVenta.DataContext = null;
                List<Venta> categorias = new List<Venta>() { buscado };
                tablaVenta.DataContext = categorias;
            }
            else
            {
                MessageBox.Show("Venta no existente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            idBuscarRventa.Clear();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpFecha1.SelectedDate.HasValue && dpFecha2.SelectedDate.HasValue)
            {
                DateTime fechaInicio = dpFecha1.SelectedDate.Value;
                DateTime fechaFin = dpFecha2.SelectedDate.Value;

                if (DateTime.Compare(fechaInicio, fechaFin) <= 0)
                {
                    var ventas = logicaVenta.Leer();
                    if (ventas != null)
                    {
                        List<Venta> filtro = ventas.Where(item => item.FechaVenta >= fechaInicio && item.FechaVenta <= fechaFin).ToList();

                        tablaVenta.DataContext = null;
                        tablaVenta.DataContext = filtro;
                    }
                    else
                    {
                        MessageBox.Show("No existen facturas", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("La fecha inicial es mayor a la fecha final", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione ambas fechas", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnDetalle_Click(object sender, RoutedEventArgs e)
        {
            if (tablaVenta.SelectedItem != null)
            {
                Venta venta = (Venta)tablaVenta.SelectedItem;
                DetalleVentaVista ventana = new DetalleVentaVista(venta.detalles);
                ventana.Show();
            }
            else
            {
                MessageBox.Show("Seleccione una factura", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarTabla();
        }
    }
}

