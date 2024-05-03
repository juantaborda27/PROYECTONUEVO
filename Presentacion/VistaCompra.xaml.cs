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
        public VistaCompra()
        {
            InitializeComponent();
            DateTime fechaActual = DateTime.Now;
            lbFecha.Content = fechaActual.ToString("dd/MM/yyyy");
        }
    }
}
