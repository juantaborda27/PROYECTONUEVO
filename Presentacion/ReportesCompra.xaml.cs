using ENTIDADES;
using Logica;
using System.Collections.Generic;
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
                DetalleCompraVista vista = new DetalleCompraVista(compra.IdCompra);
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
                    MessageBox.Show(logicaCompra.Eliminar(compra.IdCompra));
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

        private void DatePickerInitial_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerInitial.SelectedDate.HasValue)
            {

                btnSelectInitialDate.Content = datePickerInitial.SelectedDate.Value.ToString("dd/MM/yyyy");
            }

            datePickerInitial.Visibility = Visibility.Collapsed;
            datePickerInitial.SelectedDateChanged -= DatePickerInitial_SelectedDateChanged;
        }

        private void btnSelectFinalDate_Click(object sender, RoutedEventArgs e)
        {

            datePickerFinal.Visibility = Visibility.Visible;
            datePickerFinal.IsDropDownOpen = true;
            datePickerFinal.SelectedDateChanged += DatePickerFinal_SelectedDateChanged;
        }

        private void DatePickerFinal_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerFinal.SelectedDate.HasValue)
            {
                btnSelectFinalDate.Content = datePickerFinal.SelectedDate.Value.ToString("dd/MM/yyyy");
            }

            datePickerFinal.Visibility = Visibility.Collapsed;
            datePickerFinal.SelectedDateChanged -= DatePickerFinal_SelectedDateChanged;
        }

        private void btnSelectInitialDate_Click(object sender, RoutedEventArgs e)
        {
            datePickerFinal.Visibility = Visibility.Visible;
            datePickerFinal.IsDropDownOpen = true;
            datePickerFinal.SelectedDateChanged += DatePickerFinal_SelectedDateChanged;
        }

        private void BtnBuscarCompra_Click(object sender, RoutedEventArgs e)
        {

            if (idBuscarRCompra.Text.Equals("") || idBuscarRCompra.Text == null)
            {
                ActualizarTabla();
                return;
            }
            string buscar = idBuscarRCompra.Text;
            Compra buscado = logicaCompra.Buscar(buscar);

            if (buscado != null)
            {
                tbCompra.DataContext = null;
                List<Compra> categorias = new List<Compra>() { buscado };
                tbCompra.DataContext = categorias;
            }
            else
            {
                MessageBox.Show("Compra no existente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            idBuscarRCompra.Clear();

        }
    }
    }

