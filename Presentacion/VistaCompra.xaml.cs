using ENTIDADES;
using Logica;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para VistaCompra.xaml
    /// </summary>
    public partial class VistaCompra : UserControl
    {
        LogicaProveedor logicaProveedor = new LogicaProveedor();
        LogicaProducto logicaProducto = new LogicaProducto();
        LogicaCompra logicaCompra = new LogicaCompra();
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
            if (txtCodProducto.Text.Equals(""))
            {
                MessageBox.Show("El codigo de producto se encuentra vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Producto producto = logicaProducto.Buscar(txtCodProducto.Text);
            if(producto != null)
            {
                txtProducto.Text = producto.descripcion;
                txtPrecioVenta.Text = producto.precioVenta.ToString();
                txtPrecioCompra.Text = producto.precioCompra.ToString();
                txtCantidad.Text = "0";
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
            if (ValidarCamposVacios())
            {
                if (logicaProducto.Buscar(txtCodProducto.Text) != null)
                {
                    if (ValidarExistente(txtCodProducto.Text))
                    {
                        if (ValidarPrecio(txtPrecioCompra.Text, txtPrecioVenta.Text))
                        {
                            DetalleCompra detalle = new DetalleCompra();
                            id++;
                            detalle.idDetalleCompra = id;
                            detalle.producto = logicaProducto.Buscar(txtCodProducto.Text);
                            if (ValidarPrecioContenido(txtPrecioCompra.Text))
                            {
                                detalle.precioCompra = double.Parse(txtPrecioCompra.Text);
                            }
                            else
                            {
                                return;
                            }
                            if (ValidarPrecioContenido(txtPrecioVenta.Text))
                            {
                                detalle.precioVenta = double.Parse(txtPrecioVenta.Text);
                            }
                            else
                            {
                                return;
                            }
                            if (ValidarNumero(txtCantidad.Text))
                            {
                                detalle.cantidad = int.Parse(txtCantidad.Text);
                            }
                            else
                            {
                                return;
                            }
                            detalle.CalcularTotal();
                            detalles.Add(detalle);
                            ActualizarTabla();
                        }
                    }
                    else
                    {
                        MessageBox.Show("El producto ya está registrado en la tabla", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("El producto no existe", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                CamposVacios();
            }
        }
        bool ValidarPrecioContenido(string campo)
        {
            foreach (var item in campo)
            {
                if (!char.IsDigit(item) && item!='.')
                {
                    MessageBox.Show("El precio debe ser numérico", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            if (double.Parse(campo) < 0)
            {
                MessageBox.Show("El precio debe ser valores positivos", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
        bool ValidarNumero(string campo)
        {
            foreach (var item in campo)
            {
                if (!char.IsDigit(item))
                {
                    MessageBox.Show("La cantidad debe ser numérica", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            if (int.Parse(campo)<0)
            {
             MessageBox.Show("La cantidad debe ser valores positivos", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
             return false;
            }
            return true;
        }
        bool ValidarPrecio(string precioCompra,string precioVenta)
        {
            if (double.Parse(precioVenta) <= double.Parse(precioCompra))
            {
                MessageBox.Show("El precio de venta debe ser superior al precio de compra", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        bool ValidarCamposFactura()
        {
            if (cbProveedor.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un proveedor", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if(detalles.Count == 0 || detalles ==null)
            {
                MessageBox.Show("No existen productos para facturar", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
        public void ActualizarTabla()
        {
            tblVistaCompra.DataContext = null;
            tblVistaCompra.DataContext = detalles;
            double total = 0;
            foreach (var item in detalles)
            {
                total += item.total;
            }
            lbPago.Content=total.ToString();
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCamposFactura())
            {
                Compra compra = new Compra();
                string fecha = lbFecha.Content.ToString();
                compra.FechaCompra = DateTime.Parse(fecha);
                compra.proveedor = logicaProveedor.Buscar(lbDocumento.ToString());
                compra.montoTotal = double.Parse(lbPago.Content.ToString());
                compra.detalles = detalles;
                logicaCompra.Add(compra);
                MessageBox.Show("Factura registrada con exito", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        bool ValidarExistente(string idProducto)
        {
            foreach (var item in detalles)
            {
                if (item.producto.idProducto.Equals(idProducto))
                {
                    return false;
                }
            }
            return true;
        }
        bool ValidarCamposVacios()
        {
            if ( txtPrecioCompra.Text.Equals("0") || txtPrecioCompra.Text.Equals("") || txtPrecioVenta.Text.Equals("0") || txtPrecioVenta.Text.Equals("") || txtProducto.Text.Equals("") || txtCantidad.Text.Equals("0") || txtCantidad.Text.Equals(""))
            {
                return false;
            }
            return true;
        }
        void CamposVacios()
        {
            if (txtPrecioCompra.Text.Equals("") || txtPrecioCompra.Text.Equals("0"))
            {
                MessageBox.Show("Precio se encuentra vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (txtCantidad.Text.Equals("0") || txtCantidad.Text.Equals(""))
            {
                MessageBox.Show("Cantidad esta vacia", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            if (txtCodProducto.Text.Equals("0") || txtCodProducto.Text.Equals(""))
            {
                MessageBox.Show("Codigo esta vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (txtPrecioVenta.Text.Equals("0") || txtPrecioVenta.Text.Equals(""))
            {
                MessageBox.Show("Precio se encuenta vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (txtProducto.Text.Equals("0") || txtProducto.Text.Equals(""))
            {
                MessageBox.Show("Nombre se ecuentra vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (tblVistaCompra.SelectedItem != null)
            {
                var detalle = (DetalleCompra)tblVistaCompra.SelectedItem;
                foreach (var item in detalles)
                {
                    if (item.producto.descripcion.Equals(detalle.producto.descripcion))
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
