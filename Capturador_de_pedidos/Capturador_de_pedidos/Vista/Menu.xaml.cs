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
using System.Windows.Shapes;

namespace Capturador_de_pedidos.Vista
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        Usuario n = new Usuario();
        public Menu(int a, String b, String c)
        {
            n.Id = a;
            n.Nombre = b;
            n.Apellido = c;
            InitializeComponent();
            lblName.Content = n.Nombre + " " + n.Apellido;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            WAgregarCliente OAC = new WAgregarCliente();
            OAC.Show();
        }

        private void btnCapturador_Click(object sender, RoutedEventArgs e)
        {
            WCapturadorPedidos WCP = new WCapturadorPedidos(n.Id, n.Nombre + " " + n.Apellido);
            WCP.Show();
        }
    }
}
