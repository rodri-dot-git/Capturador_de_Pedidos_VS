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
using System.Windows.Shapes;

namespace Capturador_de_pedidos.Vista
{
    /// <summary>
    /// Lógica de interacción para WAgregarCliente.xaml
    /// </summary>
    public partial class WAgregarCliente : Window
    {
        public WAgregarCliente()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ControladorCliente OC = new ControladorCliente();
            Cliente c = new Cliente(txtNombreCliente.Text);
            OC.AddCliente(c);
        }
    }
}
