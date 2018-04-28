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
        public Menu(String n)
        {
            InitializeComponent();
            lblName.Content = n;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            WAgregarCliente OAC = new WAgregarCliente();
            OAC.Show();
            this.Close();
        }
    }
}
