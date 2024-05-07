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
    /// Lógica de interacción para VistaCompra.xaml
    /// </summary>
    public partial class VistaCompra : UserControl
    {
        LogicaProveedor logicaProveedor = new LogicaProveedor();
        LogicaProducto logicaProducto = new LogicaProducto();
        List<DetalleCompra> detalles = new List<DetalleCompra>();
        int id = 0;
        public VistaCompra()
        {
            InitializeComponent();
            List<Proveedor> proveedores = logicaProveedor.Leer();
            cbProveedor.ItemsSource = proveedores;
            DateTime fechaActual = DateTime.Now;
            lbFecha.Content = fechaActual.ToString("dd/MM/yyyy");
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = logicaProducto.Buscar(txtCodProducto.Text);
            if(producto != null)
            {
                txtProducto.Text = producto.descripcion;
                txtPrecioVenta.Text = producto.precioVenta.ToString();
                txtPrecioCompra.Text = producto.precioCompra.ToString();
                txtCantidad.Text = producto.cantidad.ToString();
            }
            else
            {
                MessageBox.Show("El producto no existe", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbProveedor != null && cbProveedor.SelectedItem != null)
            {
                Proveedor proveedor = (Proveedor)cbProveedor.SelectedItem;
                lbDocumento.Content = proveedor.documento;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (logicaProducto.Buscar(txtCodProducto.Text) != null)
            {
                DetalleCompra detalle = new DetalleCompra();
                id++;
                detalle.idDetalleCompra = id;
                detalle.producto = logicaProducto.Buscar(txtCodProducto.Text);
                detalle.precioVenta = double.Parse(txtPrecioVenta.Text);
                detalle.precioCompra=double.Parse(txtPrecioCompra.Text);
                detalle.cantidad = int.Parse(txtCantidad.Text);
                detalles.Add(detalle);
                ActualizarTabla();
            }
            else
            {
                MessageBox.Show("El producto no existe", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void ActualizarTabla()
        {
            tblVistaCompra.DataContext = detalles;
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (tblVistaCompra.SelectedItem != null)
            {
                var detalle = (DetalleCompra)tblVistaCompra.SelectedItem;
                foreach (var item in detalles)
                {
                    if (item.idDetalleCompra==detalle.idDetalleCompra)
                    {
                        detalles.Remove(item);
                        break;
                    }
                }
                ActualizarTabla();
            }
            else
            {
                MessageBox.Show("Seleccione un producto a eliminar", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
