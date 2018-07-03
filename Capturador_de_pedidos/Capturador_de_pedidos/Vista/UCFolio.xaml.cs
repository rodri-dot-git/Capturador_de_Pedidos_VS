using Capturador_de_pedidos.Controlador;
using Capturador_de_pedidos.Modelo;
using Capturador_de_pedidos.PDF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
    /// Lógica de interacción para UCFolio.xaml
    /// </summary>
    public partial class UCFolio : UserControl
    {
        ObservableCollection<DetalleVenta> detalles = new ObservableCollection<DetalleVenta>();
        public UCFolio()
        {
            InitializeComponent();
        }
        public UCFolio(int folio)
        {
            InitializeComponent();
            ControladorVenta CV = new ControladorVenta();
            ObservableCollection<DetalleVenta> temp = CV.BuscarPorFolio(folio);
            dgrPedido.ItemsSource = temp;
            lblCliente.Content = "Cliente: " + temp.ElementAt(0).Venta.C.Nombre;
            lblFecha.Content = "Fecha: " + temp.ElementAt(0).Venta.Date.Substring(0, 10);
            lblPedido.Content = "Pedido: " + temp.ElementAt(0).Venta.Folio.ToString();
            txtIVA.Text = temp.ElementAt(0).Venta.IVA.ToString();
            txtSubtotal.Text = temp.ElementAt(0).Venta.SubTotal.ToString();
            txtTotal.Text = temp.ElementAt(0).Venta.Total.ToString();
            detalles = temp;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ControladorVenta CV = new ControladorVenta();
            try
            {
                ObservableCollection<DetalleVenta> temp = CV.BuscarPorFolio(Int32.Parse(txtFolio.Text));
                dgrPedido.ItemsSource = temp;
                lblCliente.Content = "Cliente: " + temp.ElementAt(0).Venta.C.Nombre;
                lblFecha.Content = "Fecha: " + temp.ElementAt(0).Venta.Date.Substring(0, 10);
                lblPedido.Content = "Pedido: " + temp.ElementAt(0).Venta.Folio.ToString();
                txtIVA.Text = temp.ElementAt(0).Venta.IVA.ToString();
                txtSubtotal.Text = temp.ElementAt(0).Venta.SubTotal.ToString();
                txtTotal.Text = temp.ElementAt(0).Venta.Total.ToString();
                detalles = temp;
            }
            catch(SqlException ex)
            {
                MessageBox.Show("ERROR:" + ex.Message, "Error");
            }
            catch {
                MessageBox.Show("No encontrado", "Error");
            }
        }

        private void btnPDF_Click(object sender, RoutedEventArgs e)
        {
            ControladorVenta CV = new ControladorVenta();
            ControladorCliente CC = new ControladorCliente();
            try
            {
                if (detalles.ElementAt(0).Venta.C.Nombre != null)
                {
                    detalles.ElementAt(0).Venta.IdCliente = CC.BuscaId(detalles.ElementAt(0).Venta.C.Nombre);
                    PDFMaker.NewPedido(detalles);
                }
            }
            catch
            {
                MessageBox.Show("Error", "Error");
            }
        }
    }
}
