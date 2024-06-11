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
            List<CategoriaProducto> categorias = logicaCategoria.Leer();
            tblListaProductos1.DataContext = productos;
            cbCategoria.ItemsSource = categorias;
            cbFiltrar.ItemsSource = categorias;
            AlertaBajoStock();
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
                if (cbCategoria.SelectedItem != null)
                {
                    producto.categoriaProducto=(CategoriaProducto) cbCategoria.SelectedItem;
                }
                else
                {
                    MessageBox.Show("Seleccione una categoria", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
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
            if(txtCodigoProducto.Text == null || txtDescripcion == null || txtMinima == null || txtCodigoProducto.Text.Equals("") || txtDescripcion.Text.Equals("")||txtMinima.Text.Equals(""))
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
            if (BoxBuscarListProductos.Text.Equals("") || BoxBuscarListProductos.Text == null)
            {
                ActualizarTablaProducto();
                return;
            }
            string buscar = BoxBuscarListProductos.Text;
            Producto buscado = logicaProducto.Buscar(buscar);

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
            BoxBuscarListProductos.Clear();

        }
        void Limpiar()
        {
            txtDescripcion.Clear();
            txtCodigoProducto.Clear();
            txtMinima.Clear();
            BoxBuscarListProductos.Clear();
            ActualizarTablaProducto();
            cbFiltrar.SelectedItem = null;
            cbCategoria.SelectedItem = null;
        }

        void AlertaBajoStock()
        {
            List<Producto> filtrado = new List<Producto>();
            foreach (var item in logicaProducto.Leer())
            {
                if (item.cantidad <= item.cantidadMinima)
                {
                    filtrado.Add(item);
                }
            }
            if (filtrado.Count != 0)
            {
                MessageBoxResult result = MessageBox.Show("Existen productos con baja cantidad de stock\n¿Desea saber el listado? ", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    tblListaProductos1.DataContext = null;
                    tblListaProductos1.DataContext = filtrado;
                }
            }

        }

        private void cbFiltrar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriaProducto categoria = (CategoriaProducto)cbFiltrar.SelectedItem;
            List<Producto> filtro = new List<Producto>();
            if (cbFiltrar.SelectedItem != null )
            {
                if (logicaProducto.Leer() != null)
                {
                    foreach (var item in logicaProducto.Leer())
                    {
                        if (item.categoriaProducto.idCategoria.Equals(categoria.idCategoria))
                        {
                            filtro.Add(item);
                        }
                    }
                    tblListaProductos1.DataContext = null;
                    tblListaProductos1.DataContext = filtro;
                }
                else
                {   
                  MessageBox.Show("No existen productos", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);   
                }
            }
            
        }
    }
}
