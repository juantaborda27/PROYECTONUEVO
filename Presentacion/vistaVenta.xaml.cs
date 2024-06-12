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
using System.Net;
using System.Net.Mail;
using Infraestructura;
using System.Windows.Media.Media3D;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para vistaVenta.xaml
    /// </summary>
    public partial class vistaVenta : UserControl
    {
        LogicaProducto logicaProducto = new LogicaProducto();
        LogicaVenta logicaVenta = new LogicaVenta();
        List<DetalleVenta> detalles = new List<DetalleVenta>();
        double total = 0;
        LogicaCliente logicaCliente = new LogicaCliente();
        public vistaVenta()
        {
            InitializeComponent();
            DateTime fechaActual = DateTime.Now;
            if (logicaVenta.Leer().Count != 0)
            {
                lbIdVentas.Content = int.Parse(logicaVenta.Leer().Last().idVenta) + 1;
            }
            else
            {
                lbIdVentas.Content = "1";
            }
            lbFechaVentas.Content = fechaActual.ToString("dd/MM/yyyy");
        }
        private void BtnBuscarVenta_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdProducto.Text.Equals(""))
            {
                MessageBox.Show("El codigo de producto se encuentra vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Producto producto = logicaProducto.Buscar(txtIdProducto.Text);
            if (producto != null)
            {
                lbValor.Content = producto.precioVenta.ToString();
                lbProducto.Content = producto.descripcion;
                lbCategoria.Content = producto.categoriaProducto.descripcion;
            }
            else
            {
                MessageBox.Show("El producto no existe", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAddVentas_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCamposVacios())
            {
                if (logicaProducto.Buscar(txtIdProducto.Text) != null)
                {
                    if (ValidarExistente(txtIdProducto.Text))
                    {
                            DetalleVenta detalle = new DetalleVenta();
                            
                            detalle.idVenta = int.Parse(lbIdVentas.Content.ToString());
                            detalle.precioVenta = logicaProducto.Buscar(txtIdProducto.Text).precioVenta;
                            detalle.producto = logicaProducto.Buscar(txtIdProducto.Text);
                            if (ValidarNumero(txtCantidadVent.Text))
                            {
                                if(int.Parse(txtCantidadVent.Text)> logicaProducto.Buscar(txtIdProducto.Text).cantidad)
                                {
                                MessageBox.Show("Pocas unidades disponibles", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                                }
                                detalle.cantidad = int.Parse(txtCantidadVent.Text);
                            }
                            else
                            {
                               return;
                            }
                            double total = detalle.CalcularTotal();
                            detalle.total = total;
                        
                            detalles.Add(detalle);
                            ActualizarTabla();
                        
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
        }
        bool ValidarNombre(string campo)
        {
            foreach (var item in campo)
            {
                if(!char.IsLetter(item) && item!=' ' && item!='ñ' && item != 'Ñ')
                {
                    MessageBox.Show("Formato de nombre incorrecto", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
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
            if (int.Parse(campo) < 0)
            {
                MessageBox.Show("La cantidad debe ser valores positivos", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
        bool ValidarCamposFactura()
        {
            if (detalles.Count == 0)
            {
                MessageBox.Show("No existen productos para facturar", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (txtRecibidoVenta.Text.Length==0)
            {
                MessageBox.Show("Cantidad recibida vacía", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
        public void ActualizarTabla()
        {
            total = 0;
            tblVistaVenta.DataContext = null;
            tblVistaVenta.DataContext = detalles;
            foreach (var item in detalles)
            {
                total += item.total;
            }
            lbPagoVenta.Content = total.ToString();
        }

        private void btnRegistrarVenta_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCamposFactura())
            {
                Venta venta = new Venta();
                string fecha = lbFechaVentas.Content.ToString();
                venta.usuario = LogicaLogin.usuario;
                venta.idVenta = lbIdVentas.Content.ToString();
                venta.FechaVenta = DateTime.Parse(fecha);
                venta.montoTotal = double.Parse(lbPagoVenta.Content.ToString());
                
                venta.detalles = detalles;
              
                if(txtCedulaClienteVentas.Text.Length != 0)
                {
                    venta.cliente= logicaCliente.Buscar(txtCedulaClienteVentas.Text);

                }
                else
                {
                    MessageBox.Show("Cedula Cliente vacía", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

                }
               

                if (ValidarPrecioContenido(txtRecibidoVenta.Text))
                {
                    venta.montoPago = double.Parse(txtRecibidoVenta.Text);
                }
                else
                {
                    return;
                }
                venta.CalcularCambio();
                logicaVenta.Add(venta);
                MessageBox.Show("Factura registrada con exito", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBoxResult result = MessageBox.Show("¿Desea enviar la factura por email?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Email email = new Email();
                    email.Enviar(venta,venta.cliente.Correo);
                }
                Limpiar();
            }
        }
        bool ValidarPrecioContenido(string campo)
        {
            foreach (var item in campo)
            {
                if (!char.IsDigit(item) && item != '.')
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
            if ( txtCantidadVent.Text.Equals("0") || txtCantidadVent.Text.Equals(""))
            {
                CamposVacios();
                return false;
            }
            return true;
        }
        void CamposVacios()
        {
            if (txtIdProducto.Text.Equals("0") || txtIdProducto.Text.Equals(""))
            {
                MessageBox.Show("Codigo esta vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (txtCantidadVent.Text.Equals("0") || txtCantidadVent.Text.Equals(""))
            {
                MessageBox.Show("Cantidad esta vacia", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int i = 1;
            if (tblVistaVenta.SelectedItem != null)
            {
                var detalle = (DetalleVenta)tblVistaVenta.SelectedItem;
                foreach (var item in detalles)
                {
                    if (item.idDetalleVenta==detalle.idDetalleVenta)
                    {
                        detalles.Remove(item);
                        break;
                    }
                }
                foreach (var item in detalles)
                {
                    item.idDetalleVenta = i;
                    i++;
                }
                ActualizarTabla();
                lbCambioVenta.Content = "";
            }
            else
            {
                MessageBox.Show("Seleccione un producto a eliminar", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void txtRecibidoVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key== Key.Enter) {
                double cambio = double.Parse(txtRecibidoVenta.Text);
                if (cambio < total)
                {
                    MessageBox.Show("La cantidad recibida es menor al pago total", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                lbCambioVenta.Content = (cambio - total).ToString();
            }
            
        }

        private void btnAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            AgregarCliente vista = new AgregarCliente();
            vista.Show();
        }

        private void btnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (txtCedulaClienteVentas.Text.Equals(""))
            {
                MessageBox.Show("La cedula del cliente se encuentra vacia", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Cliente cliente = logicaCliente.Buscar(txtCedulaClienteVentas.Text);
            if (cliente != null)
            {
                txtNombre.Content = cliente.NombreCliente;
                txtCorreo.Content = cliente.Correo;
                
            }
            else
            {
                MessageBox.Show("El cliente no existe", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        void Limpiar()
        {
            txtCedulaClienteVentas.Clear();
            lbCambioVenta.Content = "";
            lbCategoria.Content = "";
            lbPagoVenta.Content = "";
            lbProducto.Content = "";
            lbValor.Content = "";
            txtCorreo.Content = "";
            txtNombre.Content = "";
            tblVistaVenta.DataContext= null;
            txtRecibidoVenta.Clear();
            lbCambioVenta.Content = "";
            txtIdProducto.Clear();
            txtCantidadVent.Clear();
            lbIdVentas.Content = int.Parse(lbIdVentas.Content.ToString()) + 1;
        }

        
    }
}
