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
                if (ValidarNumero(txtDocumentoCliente.Text))
                {
                    cliente.Documento = txtDocumentoCliente.Text;
                }
                else
                {
                    MessageBox.Show("Formato de documento incorrecto", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (ValidarNombre(txtNombreCliente.Text))
                {
                    cliente.NombreCliente = txtNombreCliente.Text;
                }
                else
                {
                    return;
                }
                if (ValidarNumero(txtTelefonoCliente.Text))
                {
                    cliente.Telefono = txtTelefonoCliente.Text;
                }
                else
                {
                    MessageBox.Show("Formato del telefono es incorrecto", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                cliente.Correo = txtCorreoCliente.Text;
                logicaCliente.Add(cliente);
                ActualizarTabla();
                Limpiar();
                MessageBox.Show("Cliente registrado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Cliente existente", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        bool ValidarNombre(string campo)
        {
            foreach (var item in campo)
            {
                if (!char.IsLetter(item) && item != ' ' && item != 'ñ' && item != 'Ñ')
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
                    return false;
                }
            }
            return true;
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
            List<Cliente> filtrado = new List<Cliente>();
            if (logicaCliente.Buscar(BoxBuscarListClientes.Text) != null)
            {
                filtrado.Add(logicaCliente.Buscar(BoxBuscarListClientes.Text));
                tblCliente.DataContext = null;
                tblCliente.DataContext = filtrado;
                return;
            }
            else
            {
                MessageBox.Show("El Cliente no existe", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnActualizarTabla_Click(object sender, RoutedEventArgs e)
        {
            ActualizarTabla();
        }
    }
}
