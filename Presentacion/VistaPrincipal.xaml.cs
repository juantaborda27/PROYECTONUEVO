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
    /// Lógica de interacción para VistaPrincipal.xaml
    /// </summary>
    public partial class VistaPrincipal : Window
    {
        public VistaPrincipal()
        {
            
            InitializeComponent();
            if (LogicaLogin.usuario.rol.Equals("Empleado"))
            {
                btnUsuario.IsEnabled = false;
                btnReportes.IsEnabled = false;
                Producto.IsEnabled = false;
            }
            lbUsuario.Content = $"Usuario:{LogicaLogin.usuario.userName}";

        }

        public void ReporteVentas_Click(object sender, RoutedEventArgs e)
        {
            frameVenta.Navigate(new Uri("ReportesVentas.xaml", UriKind.Relative));
        }

        public void ReporteCompras_Click(object sender, RoutedEventArgs e)
        {
            frameVenta.Navigate(new Uri("ReportesCompra.xaml", UriKind.Relative));
        }

        private void btnVentas_Click(object sender, RoutedEventArgs e)
        {
            frameVenta.Navigate(new Uri("vistaVenta.xaml", UriKind.Relative));

        }

        private void btnCompras_Click(object sender, RoutedEventArgs e)
        {
            frameVenta.Navigate(new Uri("VistaCompra.xaml", UriKind.Relative));
        }

        private void btrnProveedores_Click(object sender, RoutedEventArgs e)
        {
            frameVenta.Navigate(new Uri("VistaProveedor.xaml", UriKind.Relative));
        }

        

        private void btnUsuario_Click(object sender, RoutedEventArgs e)
        {
            frameVenta.Navigate(new Uri("VistaUsuario.xaml", UriKind.Relative));
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Aquí puedes escribir el código que se ejecutará cuando se produzca el evento MouseDown en la ventana
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Mostrar el Popup cuando el botón es presionado
            

        }

        private void Producto_Click(object sender, RoutedEventArgs e)
        {
            
            frameVenta.Navigate(new Uri("VistaProducto.xaml", UriKind.Relative));
        }

        private void Categoria_Click(object sender, RoutedEventArgs e)
        {
            
            frameVenta.Navigate(new Uri("vistaCategoriaProducto.xaml", UriKind.Relative));
        }
        private void Compras_Click(object sender, RoutedEventArgs e)
        {
            frameVenta.Navigate(new Uri("ReportesCompra.xaml", UriKind.Relative));
        }
        private void Ventas_Click(object sender, RoutedEventArgs e)
        {
            frameVenta.Navigate(new Uri("ReportesVentas.xaml", UriKind.Relative));
        }
        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            //reportes.IsOpen = true;
            if (btnReportes.ContextMenu != null)
            {
                btnReportes.ContextMenu.PlacementTarget = btnReportes;
                btnReportes.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
                btnReportes.ContextMenu.IsOpen = true;
               
            }
        }

        private void btrnClientes_Click(object sender, RoutedEventArgs e)
        {
            frameVenta.Navigate(new Uri("VistaCliente.xaml", UriKind.Relative));
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.ResizeMode = ResizeMode.CanResize;
            }
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            HiddenMarkTVV nuevaVista = new HiddenMarkTVV(new VistaPrincipal());
            nuevaVista.Show();
            this.Hide();
        }
    }
}
