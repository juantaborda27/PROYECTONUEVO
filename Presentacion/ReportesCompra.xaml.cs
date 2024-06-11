using ENTIDADES;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpFecha1.SelectedDate.HasValue && dpFecha2.SelectedDate.HasValue)
            {
                DateTime fechaInicio = dpFecha1.SelectedDate.Value;
                DateTime fechaFin = dpFecha2.SelectedDate.Value;

                if (DateTime.Compare(fechaInicio, fechaFin) <= 0)
                {
                    var compras = logicaCompra.Leer();
                    if (compras != null)
                    {
                        List<Compra> filtro = compras.Where(item => item.FechaCompra >= fechaInicio && item.FechaCompra <= fechaFin).ToList();

                        tbCompra.DataContext = null;
                        tbCompra.DataContext = filtro;
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
    }
    }

