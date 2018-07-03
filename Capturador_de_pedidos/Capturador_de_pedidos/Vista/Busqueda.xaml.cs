using Capturador_de_pedidos.Controlador;
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
    /// Lógica de interacción para Busqueda.xaml
    /// </summary>
    public partial class Busqueda : Window
    {
        public Busqueda()
        {
            InitializeComponent();
        }

        private void rbnFechaNombre_Checked(object sender, RoutedEventArgs e)
        {
            ControladorUserControl CUC = new ControladorUserControl();
            cbxFecha.IsEnabled = true;
            cbxNombre.IsEnabled = true;
            CUC.CargarUCGrid(grdBusqueda, new UCFechaCliente(cbxFecha.IsChecked.GetValueOrDefault(), cbxNombre.IsChecked.GetValueOrDefault()));
            
        }

        private void rbnFolio_Checked(object sender, RoutedEventArgs e)
        {
            ControladorUserControl CUC = new ControladorUserControl();
            cbxFecha.IsEnabled = false;
            cbxNombre.IsEnabled = false;
            CUC.CargarUCGrid(grdBusqueda, new UCFolio());
        }

        private void cbxFecha_Checked(object sender, RoutedEventArgs e)
        {
            ControladorUserControl CUC = new ControladorUserControl();
            CUC.CargarUCGrid(grdBusqueda, new UCFechaCliente(cbxFecha.IsChecked.GetValueOrDefault(), cbxNombre.IsChecked.GetValueOrDefault()));
        }

        private void cbxNombre_Checked(object sender, RoutedEventArgs e)
        {
            ControladorUserControl CUC = new ControladorUserControl();
            CUC.CargarUCGrid(grdBusqueda, new UCFechaCliente(cbxFecha.IsChecked.GetValueOrDefault(), cbxNombre.IsChecked.GetValueOrDefault()));
        }
    }
}
