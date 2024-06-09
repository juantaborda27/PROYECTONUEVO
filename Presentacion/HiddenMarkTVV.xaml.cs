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
    public partial class HiddenMarkTVV : Window
    {
        
        private VistaPrincipal ventanaAnterior;

        public HiddenMarkTVV(VistaPrincipal ventanaAnterior)
        {
            InitializeComponent();
            this.ventanaAnterior = ventanaAnterior;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            
            ventanaAnterior.Show();
            this.Close();
        }
    }
}
