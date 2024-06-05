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
using System.Windows.Shapes;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        LogicaLogin login=new LogicaLogin();
        
        public Login()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Usuario loguear = new Usuario();
            loguear.userName = txtUsuario.Text;
            loguear.contraseña = txtContra.Password;
            Usuario logueado = login.Loguear(loguear);
            if (logueado != null)
            {
                VistaPrincipal vistaPrincipal1 = new VistaPrincipal();
                vistaPrincipal1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario Incorrecto(Admin-1234)");
            }



        }

        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {
            Usuario loguear = new Usuario
            {
                userName = txtUsuario.Text,
                contraseña = txtContra.Password
            };

            Usuario logueado = login.Loguear(loguear);

            if (logueado != null)
            {
                VistaPrincipal vistaPrincipal1 = new VistaPrincipal();
                ConfigurarPermisos(vistaPrincipal1, logueado);
                vistaPrincipal1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario Incorrecto(Admin-1234)");
            }
        }

        private void ConfigurarPermisos(VistaPrincipal vistaPrincipal, Usuario logueado)
        {
            if (logueado.rol.Equals("Administrador"))
            {
                vistaPrincipal.btnCliente.IsEnabled = true;
                vistaPrincipal.btnCompras.IsEnabled = true;
                vistaPrincipal.btnVentas.IsEnabled = true;
                vistaPrincipal.btnUsuario.IsEnabled = true;
                vistaPrincipal.btnReportes.IsEnabled = true;
            }
            else if (logueado.rol.Equals("Empleado"))
            {
                vistaPrincipal.btnCliente.IsEnabled = true;
                vistaPrincipal.btnCompras.IsEnabled = false;
                vistaPrincipal.btnVentas.IsEnabled = true;
                vistaPrincipal.btnUsuario.IsEnabled = false;
                vistaPrincipal.btnReportes.IsEnabled = false;
            }
            else
            {
                // Default or other roles logic
                vistaPrincipal.btnCliente.IsEnabled = false;
                vistaPrincipal.btnCompras.IsEnabled = false;
                vistaPrincipal.btnVentas.IsEnabled = false;
                vistaPrincipal.btnUsuario.IsEnabled = false;
                vistaPrincipal.btnReportes.IsEnabled = false;
            }
        }

        //private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        //{
        //    Usuario loguear = new Usuario();
        //    loguear.userName = txtUsuario.Text;
        //    loguear.contraseña = txtContra.Password;
        //    Usuario logueado = login.Loguear(loguear);
        //    VistaPrincipal vistaPrincipal1 = new VistaPrincipal();
        //    permmiso(vistaPrincipal1, loguear);
        //    if (logueado != null)
        //    {
        //        vistaPrincipal1.Show();
        //        this.Hide();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Usuario Incorrecto(Admin-1234)");
        //    }
        //}

        //private void permmiso(VistaPrincipal vistaPrincipal1, Usuario loguear)
        //{

        //    if (loguear.rol.Equals("Administrador"))
        //    {

        //        vistaPrincipal1.btnCliente.IsEnabled = true;
        //        vistaPrincipal1.btnCompras.IsEnabled = true;
        //        vistaPrincipal1.btnVentas.IsEnabled = true;
        //        vistaPrincipal1.btnCliente.IsEnabled = true;
        //        vistaPrincipal1.btnUsuario.IsEnabled = true;
        //        vistaPrincipal1.btnReportes.IsEnabled = true;
        //        vistaPrincipal1.Show();
        //        this.Hide();
        //    }
        //    else if(loguear.rol.Equals("Empleado"))
        //    {
        //        vistaPrincipal1.btnCliente.IsEnabled = false;
        //        vistaPrincipal1.btnCompras.IsEnabled = false;
        //        vistaPrincipal1.btnVentas.IsEnabled = true;
        //        vistaPrincipal1.btnCliente.IsEnabled = true;
        //        vistaPrincipal1.btnCompras.IsEnabled = false;
        //        vistaPrincipal1.btnUsuario.IsEnabled = false;
        //        vistaPrincipal1.btnReportes.IsEnabled = false;
        //    }

        //}
    }
}
