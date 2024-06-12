using ENTIDADES;
using Logica;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para vistaCategoriaProducto.xaml
    /// </summary>
    public partial class vistaCategoriaProducto : UserControl
    {
        LogicaCategoria logicaCategoria = new LogicaCategoria();
        public vistaCategoriaProducto()
        {
            InitializeComponent();
            List<CategoriaProducto> categorias = logicaCategoria.Leer();
            tblListaCategoria.DataContext = categorias;
        }

        private void BtnGuuardar(object sender, RoutedEventArgs e)
        {
            if (ValidarCamposVacios()==false)
            {
                MessageBox.Show("Existen Campos Vacios", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            CategoriaProducto categoria = new CategoriaProducto();
            if (logicaCategoria.Buscar(int.Parse(txtIdCategoria.Text)) == null)
            {
                categoria.idCategoria = int.Parse(txtIdCategoria.Text);
                categoria.descripcion = txtDescripProducto.Text;
                logicaCategoria.Add(categoria);
                ActualizarTabla();
                txtDescripProducto.Clear();
                MessageBox.Show("Categoria registrada correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Categoria existente", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
        void ActualizarTabla()
        {
            List<CategoriaProducto> categorias = logicaCategoria.Leer();
            tblListaCategoria.DataContext = categorias;
        }
        private void BtnLIMPPIAR(object sender, RoutedEventArgs e)
        {
            txtDescripProducto.Clear();
        }

        private void BtnEEliminar(object sender, RoutedEventArgs e)
        {
            if (tblListaCategoria.SelectedItem != null)
            {
                var categoria = (CategoriaProducto)tblListaCategoria.SelectedItem;
                MessageBoxResult result = MessageBox.Show("¿Estás seguro que deseas continuar?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show(logicaCategoria.Eliminar(categoria.idCategoria));
                }
                ActualizarTabla();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna categoria", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        private void BtnBuscarCategoria_Click(object sender, RoutedEventArgs e)
        {
            if (txtBuscarListaCategoria.Text.Equals("") || txtBuscarListaCategoria.Text == null)
            {
                ActualizarTabla();
                return;
            }
            int buscar = int.Parse(txtBuscarListaCategoria.Text);
            CategoriaProducto buscado = logicaCategoria.Buscar(buscar);

            if (buscado != null)
            {
                tblListaCategoria.DataContext = null;
                List<CategoriaProducto> categorias = new List<CategoriaProducto>() { buscado };
                tblListaCategoria.DataContext = categorias;
            }
            else
            {
                MessageBox.Show("Categoria no existente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            txtBuscarListaCategoria.Clear();
        }
        bool ValidarCamposVacios()
        {
            if (txtDescripProducto.Text == null || txtDescripProducto.Text.Equals(""))
            {
                return false;
            }
            return true;
        }

        private void BtnActualizarTabla_Click(object sender, RoutedEventArgs e)
        {
            ActualizarTabla();
        }
    }
}
