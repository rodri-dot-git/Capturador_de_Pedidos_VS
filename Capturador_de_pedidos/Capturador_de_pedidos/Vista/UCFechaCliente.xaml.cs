using Capturador_de_pedidos.Controlador;
using Capturador_de_pedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace Capturador_de_pedidos.Vista
{
    /// <summary>
    /// Lógica de interacción para UCFechaCliente.xaml
    /// </summary>
    public partial class UCFechaCliente : UserControl
    {
        Boolean fecha, nombre;
        public UCFechaCliente(Boolean fecha, Boolean nombre)
        {
            InitializeComponent();
            if (!fecha){ date1.IsEnabled = false; date2.IsEnabled = false; }
            if (!nombre) cmbCliente.IsEnabled = false;
            else
            {
                ControladorCliente CC = new ControladorCliente();
                cmbCliente.ItemsSource = CC.GetAllClientes();
            }
            this.fecha = fecha;
            this.nombre = nombre;
        }

        private void dgrPedidosBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrPedidosBusqueda.SelectedItems != null)
            {
                int v = 0;
                try
                {
                    DataGrid dataGrid = sender as DataGrid;
                    Venta rowView = (Venta)dataGrid.SelectedItem;
                    if (rowView != null) v = rowView.Folio;
                }
                catch { }
                
                if (v != 0)
                {
                    ResultadosBusqueda RB = new ResultadosBusqueda(v);
                    RB.Show();
                }
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ControladorVenta CV = new ControladorVenta();
            ObservableCollection<Venta> temp = new ObservableCollection<Venta>();
            double tot = 0;
            if (!nombre && fecha)
            {
                temp = CV.BuscarPorFecha(date1.SelectedDate.Value.Date,
                date2.SelectedDate.Value.Date);
            }
            if (nombre && !fecha && cmbCliente.SelectedItem != null)
            {
                temp = CV.BuscarPorNombre(cmbCliente.SelectedItem.ToString());
            }
            if (nombre && fecha && cmbCliente.SelectedItem != null)
            {
                temp = CV.BuscarPorNombreFecha(cmbCliente.SelectedItem.ToString(), date1.SelectedDate.Value.Date,
                date2.SelectedDate.Value.Date);
            }
            dgrPedidosBusqueda.ItemsSource = temp;
            for(int i = 0; i < temp.Count; i++)
            {
                tot += temp.ElementAt(i).Total;
            }
            tot = Math.Round(tot, 2);
            txtTotal.Text = tot.ToString();
            
        }
    }
}
