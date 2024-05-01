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
        }

        private void btnVentas_Click(object sender, RoutedEventArgs e)
        {
            frameVenta.Navigate(new Uri("vistaVenta.xaml", UriKind.Relative));

        }

        private void btnCompras_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btrnProveedores_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUsuario_Click(object sender, RoutedEventArgs e)
        {

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
        


    }
}
