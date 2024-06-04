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
using ENTIDADES;
using Logica;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para VistaCliente.xaml
    /// </summary>
    public partial class VistaCliente : UserControl
    {
        LogicaCliente logicaCliente = new LogicaCliente();
        public VistaCliente()
        {
            InitializeComponent();
            List<Cliente> clientes = logicaCliente.Leer();
            tblCliente.DataContext = clientes;
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
                ActualizarTabla();
                Limpiar();
                MessageBox.Show("Cliente registrado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Cliente existente", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        void ActualizarTabla()
        {
            List<Cliente> clientes = logicaCliente.Leer();
            tblCliente.DataContext = clientes;
        }

        void Limpiar()
        {
            txtDocumentoCliente.Clear();
            txtNombreCliente.Clear();
            txtTelefonoCliente.Clear();
            txtCorreoCliente.Clear();
        }


        bool ValidarCamposVacios()
        {
            if (txtDocumentoCliente.Text == null || txtDocumentoCliente.Text.Equals("") || txtNombreCliente.Text == null || txtNombreCliente.Text.Equals("") || txtCorreoCliente.Text == null || txtCorreoCliente.Text.Equals("") || txtTelefonoCliente.Text == null || txtTelefonoCliente.Text.Equals(""))
            {
                return false;
            }
            return true;
        }

        private void btnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (tblCliente.SelectedItem != null)
            {
                var cliente = (Cliente)tblCliente.SelectedItem;
                MessageBoxResult result = MessageBox.Show("¿Estás seguro que deseas continuar?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show(logicaCliente.Eliminar(cliente.Documento));
                }
                ActualizarTabla();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna categoria", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void btnLimpiarCliente_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void BtnBuscarListProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (BoxBuscarListClientes.Text.Equals("") || BoxBuscarListClientes.Text == null)
            {
                ActualizarTabla();
                return;
            }
            Cliente buscado = logicaCliente.Buscar(txtDocumentoCliente.Text);

            if (buscado != null)
            {
                tblCliente.DataContext = null;
                List<Cliente> clientes = new List<Cliente>() { buscado };
                tblCliente.DataContext = clientes;
            }
            else
            {
                MessageBox.Show("Cliente no existente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            BoxBuscarListClientes.Clear();
        }
    }
}
