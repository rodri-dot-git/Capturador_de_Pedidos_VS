using Capturador_de_pedidos.Controlador;
using Capturador_de_pedidos.Modelo;
using Capturador_de_pedidos.PDF;
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
        ObservableCollection<DetalleVenta> collection = new ObservableCollection<DetalleVenta>();
        ControladorCliente CC = new ControladorCliente();
        Articulo art;
        Venta v = new Venta();
        DetalleVenta dv = new DetalleVenta();
        int index = -1;
        public WCapturadorPedidos(int id, String name)
        {
            InitializeComponent();
            v.IdUsuario = id;
            lblUser.Content = "Vendedor: " + name;
            dgrPedido.ItemsSource = collection;
            cmbClientes.ItemsSource = CC.GetAllClientes();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            ControladorArticulo CA = new ControladorArticulo();
            art = CA.getArticulo(txtCodigo.Text);
            if (art != null)
            {
                txtPrecio.Text = art.Precio.ToString();
                dv.Venta = v;
                dv.Articulo = art;
                dv.Cantidad = Int32.Parse(txtCantidad.Text);
                dv.PrecioVenta = art.Precio;
                dv.SubTotal = dv.PrecioVenta * dv.Cantidad;
                collection.Add(new DetalleVenta(v, art, Int32.Parse(txtCantidad.Text), art.Precio, Math.Round(dv.PrecioVenta * dv.Cantidad, 2)));
                collection.OrderByDescending(x => x.Articulo.Marca.Nombre);
                dgrPedido.ItemsSource = collection;
                setTotal();
                v.SubTotal = Double.Parse(txtSubTotal.Text);
                v.IVA = Double.Parse(txtIVA.Text);
                v.Total = Double.Parse(txtTotal.Text);
            }
            dgrPedido.ItemsSource = null;
            dgrPedido.ItemsSource = collection;
        }

        private void btnMas_Click(object sender, RoutedEventArgs e)
        {
            if (txtCantidad.Text != "")
                txtCantidad.Text = (Int32.Parse(txtCantidad.Text) + 1).ToString();
            else txtCantidad.Text = "1";
        }

        private void btnMenos_Click(object sender, RoutedEventArgs e)
        {
            if (txtCantidad.Text != "" && Int32.Parse(txtCantidad.Text) > 1)
                txtCantidad.Text = (Int32.Parse(txtCantidad.Text) - 1).ToString();
            else txtCantidad.Text = "1";
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ControladorVenta CV = new ControladorVenta();
                if (cmbClientes.SelectedValue != null) v.IdCliente = CC.BuscaId(cmbClientes.SelectedValue.ToString());
                else v.IdCliente = 0;
                if (v.IdCliente != 0)
                {
                    CV.InsertarDatos(v.IdUsuario, v.IdCliente, collection);
                    PDFMaker.NewPedido(collection);
                    Close();
                }
        }

        private void cmbClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        public void setTotal()
        {
            double st=0;
            for (int i = 0; i < collection.Count; i++) st += collection.ElementAt(i).SubTotal;
            st = Math.Round(st, 2);
            txtSubTotal.Text = st.ToString();
            txtIVA.Text = Math.Round(Double.Parse(txtSubTotal.Text) * .16, 2).ToString();
            txtTotal.Text = Math.Round(Double.Parse(txtSubTotal.Text) + Double.Parse(txtIVA.Text), 2).ToString();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            ControladorArticulo CA = new ControladorArticulo();
            txtDescripcon.Text = CA.getArticuloLike(txtCodigo.Text);
        }

        private void dgrPedido_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataGridRow row1 = e.Row;
            int row_index = ((DataGrid)sender).ItemContainerGenerator.IndexFromContainer(row1);
            index = row_index;
            if (index >= 0)
            {
                collection.ElementAt(row_index).SubTotal = collection.ElementAt(row_index).Cantidad * collection.ElementAt(row_index).PrecioVenta;
                setTotal();
                if (collection.ElementAt(row_index).Cantidad == 0) collection.RemoveAt(row_index);
            }

        }

        private void dgrPedido_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            DataGridRow row1 = e.Row;
            int row_index = ((DataGrid)sender).ItemContainerGenerator.IndexFromContainer(row1);
            index = row_index;
            if (index >= 0)
            {
                collection.ElementAt(row_index).SubTotal = collection.ElementAt(row_index).Cantidad * collection.ElementAt(row_index).PrecioVenta;
                setTotal();
                if (collection.ElementAt(row_index).Cantidad == 0) collection.RemoveAt(row_index);
            }
        }

        private void dgrPedido_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }
        private void dgrPedido_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void dgrPedido_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            dgrPedido.ItemsSource = null;
            dgrPedido.ItemsSource = collection;
        }

        private void dgrPedido_CurrentCellChanged(object sender, EventArgs e)
        {
            if(index >= 0)
            {
                int row_index = index;
                collection.ElementAt(row_index).SubTotal = collection.ElementAt(row_index).Cantidad * collection.ElementAt(row_index).PrecioVenta;
                setTotal();
                if (collection.ElementAt(row_index).Cantidad == 0) collection.RemoveAt(row_index);
                dgrPedido.ItemsSource = null;
                dgrPedido.ItemsSource = collection;
            }
        }
    }
}
