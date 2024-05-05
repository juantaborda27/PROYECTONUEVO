using ENTIDADES;
using Logica;
using System;
using System.Collections;
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
        LogicaCategoria logicaCategoria = new LogicaCategoria();
        LogicaProducto logicaProducto = new LogicaProducto();
        public VistaProducto()
        {
            InitializeComponent();
            List<Producto> productos = logicaProducto.Leer();
            List<CategoriaProducto> categoriaProducto = logicaCategoria.Leer();
            cbCategoria.ItemsSource = categoriaProducto;
            tblListaProductos1.DataContext = productos;
        }

        private void Btnguardar(object sender, RoutedEventArgs e)
        {
            if (ValidarCanmposVacios()==false)
            {
                MessageBox.Show("Existen Campos Vacios", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Producto producto = new Producto();
            if (logicaProducto.Buscar(txtCodigoProducto.Text) == null )
            {
                if (ValidarNumero(txtCodigoProducto.Text))
                {
                    producto.idProducto = txtCodigoProducto.Text;
                }
                else
                {
                    MessageBox.Show("El código solo puede contener números", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                producto.descripcion = txtDescripcion.Text;
                if (ValidarNumero(txtMinima.Text) && int.Parse(txtMinima.Text)>0 && int.Parse(txtMinima.Text)<=30)
                {
                    producto.cantidadMinima = int.Parse(txtMinima.Text);
                }
                else
                {
                    MessageBox.Show("La cantidad solo puede contener números y un máximo de 30 elementos", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                producto.categoriaProducto = ((CategoriaProducto)cbCategoria.SelectedItem);
                logicaProducto.Add(producto);
                ActualizarTablaProducto();
                Limpiar();
                MessageBox.Show("Producto registrado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Producto existente", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        
        bool ValidarNumero(string campo)
        {
            foreach (var item in campo)
            {
                if (!char.IsDigit(item)) { 
                    return false;
                }
            } 
            return true;
        }
        bool ValidarCanmposVacios()
        {
            if(txtCodigoProducto.Text == null || txtDescripcion == null || txtMinima == null )
            {
                return false;
            }
            return true;
        }
        
        void ActualizarTablaProducto()
        {
            List<Producto> productos = logicaProducto.Leer();
            tblListaProductos1.DataContext = productos;
        }

        private void Btnlimpiar(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Btneliminar(object sender, RoutedEventArgs e)
        {
            if (tblListaProductos1.SelectedItem != null)
            {
                var producto = (Producto)tblListaProductos1.SelectedItem;
                MessageBoxResult result = MessageBox.Show("¿Estás seguro que deseas continuar?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show(logicaProducto.Eliminar(producto.idProducto));

                }
                ActualizarTablaProducto();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna categoria", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        private void buscaR(object sender, RoutedEventArgs e)
        {
            if(BoxBuscarListProductos.Text.Equals("") || BoxBuscarListProductos.Text == null)
            {
                ActualizarTablaProducto();
                return;            }
            Producto buscado = logicaProducto.Buscar(BoxBuscarListProductos.Text);
            
            if (buscado != null)
            {
                tblListaProductos1.DataContext = null;
                List<Producto> productos = new List<Producto>() { buscado };
                tblListaProductos1.DataContext = productos;
            }
            else
            {
                MessageBox.Show("Producto no existente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Limpiar();
            
        }
        void Limpiar()
        {
            txtDescripcion.Clear();
            txtCodigoProducto.Clear();
            txtMinima.Clear();
            cbCategoria.SelectedItem = null;
            BoxBuscarListProductos.Clear();
        }
        
        
    }
}
