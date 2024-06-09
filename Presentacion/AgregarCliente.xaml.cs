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
    /// Lógica de interacción para AgregarCliente.xaml
    /// </summary>
    public partial class AgregarCliente : Window
    {
        LogicaCliente logicaCliente = new LogicaCliente();
        public AgregarCliente()
        {
            InitializeComponent();
        }

        private void btnGuardarProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCamposVacios() == false)
            {
                MessageBox.Show("Existen Campos Vacios", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Cliente cliente = new Cliente();
            if (logicaCliente.Buscar(txtDocumentoCliente.Text) == null)
            {

                cliente.Documento = txtDocumentoCliente.Text;
                cliente.NombreCliente = txtNombreCliente.Text;
                cliente.Telefono = txtTelefonoCliente.Text;
                cliente.Correo = txtCorreoCliente.Text;
                logicaCliente.Add(cliente);
                MessageBox.Show("Cliente registrado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Cliente existente", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        bool ValidarCamposVacios()
        {
            if (txtDocumentoCliente.Text == null || txtDocumentoCliente.Text.Equals("") || txtNombreCliente.Text == null || txtNombreCliente.Text.Equals("") || txtCorreoCliente.Text == null || txtCorreoCliente.Text.Equals("") || txtTelefonoCliente.Text == null || txtTelefonoCliente.Text.Equals(""))
            {
                return false;
            }
            return true;
        }

        private void btnCerrarPestaña_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
