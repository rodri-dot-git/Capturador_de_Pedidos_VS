using Capturador_de_pedidos.Controlador;
using Capturador_de_pedidos.Modelo;
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

namespace Capturador_de_pedidos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            ControladorUsuario login = new ControladorUsuario();
            if (login.Login(txtUser.Text, pwdUser.Password.ToString()).Nombre != "")
            {
                Usuario u = new Usuario();
                int a = u.Id;
                String b = u.Nombre;
                String c = u.Apellido;
                Vista.Menu OM = new Vista.Menu(a, b, c);
                OM.Show();
                this.Close();
            }
        }
    }
}
