using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// Lógica de interacción para VistaProducto.xaml
    /// </summary>
    public partial class VistaProducto : UserControl
    {
        public VistaProducto()
        {
            InitializeComponent();
            List<string> elementos = new List<string>
        {
            "Elemento 1",
            "Elemento 2",
            "Elemento 3"
        };

            // Asignar la lista de elementos al ComboBox
            cbCategoria.ItemsSource = elementos;
        }

        private void Btnguardar(object sender, RoutedEventArgs e)
        {
            Producto nuevoProducto = new Producto();
            nuevoProducto.idProducto = txtCodigoProducto.Text;
            nuevoProducto.descripcion = txtDescripcion.Text;
            if (ValidaNumeros())
            {
                nuevoProducto.cantidadMinima = int.Parse(txtMinima.Text);
            }
            else
            {
                MessageBox.Show("Error cantidad minima solo admite numero", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
         bool ValidaNumeros()
        {
            foreach (char item in txtMinima.Text)
            {
                if (!Char.IsDigit(item))
                {
                    return false;
                }
            }
            return true;
        }
        private void Btnlimpiar(object sender, RoutedEventArgs e)
        {

        }

        private void Btneliminar(object sender, RoutedEventArgs e)
        {

        }


        private void buscaR(object sender, RoutedEventArgs e)
        {

        }
    }
}
