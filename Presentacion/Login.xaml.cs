﻿using ENTIDADES;
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
                MessageBox.Show("Usuario Incorrecto");
            }



        }
    }
}
