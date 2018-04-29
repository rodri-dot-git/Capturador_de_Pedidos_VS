using Capturador_de_pedidos.Controlador;
using Capturador_de_pedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para WCapturadorPedidos.xaml
    /// </summary>
    public partial class WCapturadorPedidos : Window
    {
        List<DetalleVenta> detalles = new List<DetalleVenta>();
        ObservableCollection<DetalleVenta> collection = new ObservableCollection<DetalleVenta>();
        ControladorCliente CC = new ControladorCliente();
        Articulo art;
        Venta v = new Venta();
        DetalleVenta dv = new DetalleVenta();
        public WCapturadorPedidos(int id, String name)
        {
            InitializeComponent();
            v.IdUsuario = id;
            lblUser.Content = "Usuario: " + name;
            dgrPedido.ItemsSource = collection;
            cmbClientes.ItemsSource = CC.GetAllClientes();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            ControladorArticulo CA = new ControladorArticulo();
            art = CA.getArticulo(txtCodigo.Text);
            txtPrecio.Text = art.Precio.ToString();
            dv.Venta = v;
            dv.Articulo = art;
            dv.Cantidad = Int32.Parse(txtCantidad.Text);
            dv.PrecioVenta = art.Precio;
            dv.SubTotal = dv.PrecioVenta * dv.Cantidad; 
            detalles.Add(dv);
            collection.Add(dv);
        }

        private void btnMas_Click(object sender, RoutedEventArgs e)
        {
            if (txtCantidad.Text != "")
                txtCantidad.Text = (Int32.Parse(txtCantidad.Text) + 1).ToString();
            else txtCantidad.Text = "1";
        }

        private void btnMenos_Click(object sender, RoutedEventArgs e)
        {
            if (txtCantidad.Text != "" && Int32.Parse(txtCantidad.Text) > 0)
                txtCantidad.Text = (Int32.Parse(txtCantidad.Text) - 1).ToString();
            else txtCantidad.Text = "0";
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ControladorVenta CV = new ControladorVenta();
            if (v.IdCliente != 0)
            {
                CV.InsertarDatos(v.IdUsuario, v.IdCliente, detalles);
                Close();
            }
            else MessageBox.Show("No ha seleccionado un cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void cmbClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            v.IdUsuario = CC.BuscaId(cmbClientes.SelectedValue.ToString());
        }
    }
}
