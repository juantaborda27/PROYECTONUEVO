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
    /// Lógica de interacción para VistaProveedor.xaml
    /// </summary>
    public partial class VistaProveedor : UserControl
    {
        LogicaProveedor logicaProveedor = new LogicaProveedor();
        public VistaProveedor()
        {
            InitializeComponent();
            List<Proveedor> proveedores = logicaProveedor.Leer();
            tblListaProveedor.DataContext = proveedores;
        }

        private void btnGuardarProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCamposVacios() == false)
            {
                MessageBox.Show("Existen Campos Vacios", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Proveedor proveedor = new Proveedor();
            if (logicaProveedor.Buscar(TxtNumDocumento.Text) == null)
            {
                if (ValidarNumero(TxtNumDocumento.Text))
                {
                    proveedor.documento = TxtNumDocumento.Text;
                    
                }
                else
                {
                    MessageBox.Show("El Documento debe ser solamente números", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (ValidarLetras(TxtNombreProveedor.Text))
                {
                    proveedor.NombreProveedor = TxtNombreProveedor.Text;
                }
                else
                {
                    MessageBox.Show("El Nombre debe ser solamente letras", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (ValidarNumero(TxtTelefono.Text))
                {
                    proveedor.telefono = TxtTelefono.Text;
                }
                else
                {
                    MessageBox.Show("El Telefono debe ser solamente números", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                logicaProveedor.Add(proveedor);
                ActualizarTablaProveedor();
                MessageBox.Show("Proveedor registrado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();    
            }
            else
            {
                MessageBox.Show("Proveedor existente", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool ValidarLetras(string campo)
        {
            foreach (var item in campo)
            {
                if (!char.IsLetter(item)&& item !=' ')
                {
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

        private void btnLimpiarProveedor_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnEliminarProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (tblListaProveedor.SelectedItem != null)
            {
                var proveedorEliminar = (Proveedor)tblListaProveedor.SelectedItem;
                MessageBoxResult result = MessageBox.Show("¿Estás seguro que deseas continuar?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show(logicaProveedor.Eliminar(proveedorEliminar.documento));

                }
                ActualizarTablaProveedor();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun Proveedor", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        void ActualizarTablaProveedor()
        {
            List<Proveedor> proveedores = logicaProveedor.Leer();
            tblListaProveedor.DataContext = proveedores;
        }

        bool ValidarCamposVacios()
        {
            if(TxtNombreProveedor.Text==null || TxtNumDocumento.Text == null || TxtTelefono.Text == null || TxtNombreProveedor.Text.Equals("")||TxtNumDocumento.Text.Equals("")||TxtTelefono.Text.Equals(""))
            {
                return false;
            }
            return true;
        }
        void Limpiar()
        {
            TxtNombreProveedor.Clear();
            TxtNumDocumento.Clear();
            TxtTelefono.Clear();
            BoxBuscarListProveedor.Clear();
        }

        private void BtnBuscarListProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (BoxBuscarListProveedor.Text.Equals("") || BoxBuscarListProveedor.Text == null)
            {
                ActualizarTablaProveedor();
                return;
            }
            Proveedor buscado = logicaProveedor.Buscar(BoxBuscarListProveedor.Text);

            if (buscado != null)
            {
                tblListaProveedor.DataContext = null;
                List<Proveedor> proveedors = new List<Proveedor>() { buscado };
                tblListaProveedor.DataContext = proveedors;
            }
            else
            {
                MessageBox.Show("Proveedor no existente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Limpiar();
        }
    }
}
