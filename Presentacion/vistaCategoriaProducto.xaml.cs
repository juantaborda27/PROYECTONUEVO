﻿using ENTIDADES;
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
            CategoriaProducto categoria = new CategoriaProducto();
            if (logicaCategoria.Buscar(txtDescripProducto.Text) == null)
            {
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
                    MessageBox.Show(logicaCategoria.Eliminar(categoria.descripcion));
                    
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
            
        }
    }
}