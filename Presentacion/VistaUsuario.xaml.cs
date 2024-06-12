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
    /// Lógica de interacción para VistaUsuario.xaml
    /// </summary>
    public partial class VistaUsuario : UserControl
    {
        LogicaUsuario logicaUsuario = new LogicaUsuario();
        public VistaUsuario()
        {
            InitializeComponent();
            List<Usuario> usuarios = logicaUsuario.Leer();
            tblUsuarios.DataContext = usuarios;
            cbRol.Items.Add("Administrador");
            cbRol.Items.Add("Empleado");
            cbFiltrar.Items.Add("Administrador");
            cbFiltrar.Items.Add("Empleado");
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
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            if (ValidarCamposVacios() == false)
            {
                MessageBox.Show("Existen Campos Vacios", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (logicaUsuario.Buscar(TxtIdUsuario.Text) == null)
            {
                if (ValidarNumero(TxtIdUsuario.Text))
                {
                    usuario.cedula = TxtIdUsuario.Text;
                }
                else
                {
                    MessageBox.Show("El usuario solo debe ser numero", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;

                }
                if (ValidarNombre(TxtNombreUs.Text))
                {
                    usuario.nombre = TxtNombreUs.Text;
                }
                else
                {
                   
                    MessageBox.Show("El nombre solo acepta letras", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (TxtUsernameUs.Text.Count()!=0)
                {
                    MessageBox.Show("Username se ecuentra vacio", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    usuario.userName = TxtUsernameUs.Text;
                }
                if (ValidarNumero(TxtTelefonoUs.Text))
                {
                    usuario.telefono = TxtTelefonoUs.Text;
                }
                else
                {
                    MessageBox.Show("El Telefono solo pueden ser numeros", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                usuario.contraseña = TxtContraseñaUs.Text;
                //Definicion
                if (cbRol.SelectedItem.ToString().Equals("Administrador"))
                {
                    usuario.rol = "Administrador";
                }
                else
                {
                    usuario.rol = "Empleado";
                }
                logicaUsuario.Add(usuario);
                ActualizarTablaUsuario();
                Limpiar();
                MessageBox.Show("Usuario registrado correctamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Usuario existente", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (tblUsuarios.SelectedItem != null)
            {
                var usuario = (Usuario)tblUsuarios.SelectedItem;
                MessageBoxResult result = MessageBox.Show("¿Estás seguro que deseas continuar?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show(logicaUsuario.Eliminar(usuario.cedula));

                }
                ActualizarTablaUsuario();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun usuario", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        void ActualizarTablaUsuario()
        {
            List<Usuario> usuarios = logicaUsuario.Leer();
            tblUsuarios.DataContext = usuarios;
        }

        bool ValidarCamposVacios()
        {
            if (cbRol.SelectedItem == null || TxtIdUsuario.Text.Count()==0 || TxtNombreUs.Text.Count() == 0 || TxtUsernameUs.Text.Count() == 0 || TxtContraseñaUs.Text.Count() == 0 || TxtTelefonoUs.Text.Count() == 0)
            {
                return false;
            }
            return true;
        }

        void Limpiar()
        {
            TxtIdUsuario.Clear();
            TxtNombreUs.Clear();
            TxtUsernameUs.Clear();
            TxtContraseñaUs.Clear();
            TxtTelefonoUs.Clear();
            cbRol.SelectedItem = null;
            cbFiltrar.SelectedItem = null;
            txtBuscarUsuarios.Clear();
            ActualizarTablaUsuario();
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

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            List<Usuario> filtrado = new List<Usuario>();
            if (logicaUsuario.Buscar(txtBuscarUsuarios.Text) != null)
            {
                filtrado.Add(logicaUsuario.Buscar(txtBuscarUsuarios.Text));
                tblUsuarios.DataContext = null;
                tblUsuarios.DataContext = filtrado ;
                return;
            }
            else
            {
                MessageBox.Show("El usuario no existe", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void cbFiltrar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Usuario> filtrado = new List<Usuario>();
            if (logicaUsuario.Leer() != null)
            {
                if (cbFiltrar.SelectedItem == null)
                {
                    ActualizarTablaUsuario();
                    return;
                }
                if (cbFiltrar.SelectedItem.Equals("Administrador"))
                {

                    foreach (var item in logicaUsuario.Leer())
                    {
                        if (item.rol.Equals("Administrador"))
                        {
                            filtrado.Add(item);
                        }

                    }
                    tblUsuarios.DataContext = null;
                    tblUsuarios.DataContext = filtrado;
                }
                else
                {
                    foreach (var item in logicaUsuario.Leer())
                    {
                        if (item.rol.Equals("Empleado"))
                        {
                            filtrado.Add(item);
                        }
                    }
                    tblUsuarios.DataContext = null;
                    tblUsuarios.DataContext = filtrado;
                }
            }
            
        }

        private void BtnActualizarTabla_Click(object sender, RoutedEventArgs e)
        {
            ActualizarTablaUsuario();
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (logicaUsuario.Buscar(TxtIdUsuario.Text) != null)
            {
                Usuario usuario = logicaUsuario.Buscar(TxtIdUsuario.Text);
                if (TxtNombreUs.Text.Count()!=0)
                {
                    if (ValidarNombre(TxtNombreUs.Text))
                    {
                        usuario.nombre = txtBuscarUsuarios.Text;
                    }
                    else
                    {
                        return;
                    }
                    
                }
                if (TxtUsernameUs.Text.Count()!=0)
                {
                    usuario.userName = TxtUsernameUs.Text;
                }
                if (TxtTelefonoUs.Text.Count() != 0)
                {
                    if (ValidarNumero(TxtTelefonoUs.Text))
                    {
                        usuario.telefono = TxtTelefonoUs.Text;
                    }
                    else
                    {
                        MessageBox.Show("El telefono debe contener valores numéricos", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                if (TxtContraseñaUs.Text.Count() != 0)
                {
                    usuario.contraseña = TxtContraseñaUs.Text;
                }
                if (cbRol.SelectedItem != null)
                {
                    if (cbRol.SelectedItem.ToString().Equals("Administrador"))
                    {
                        usuario.rol = "Administrador";
                    }
                    else
                    {
                        usuario.rol = "Empleado";
                    }
                }
                MessageBox.Show(logicaUsuario.Actualizar(usuario), "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                ActualizarTablaUsuario();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Usuario no existe", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}

