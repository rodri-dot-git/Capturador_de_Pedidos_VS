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
    /// Lógica de interacción para AgregarArticulo.xaml
    /// </summary>
    public partial class AgregarArticulo : Window
    {
        public AgregarArticulo()
        {
            InitializeComponent();
            string[] marca = {  "Truper",
                                "Pretul",
                                "Voltek",
                                "Fiero",
                                "Foset",
                                "Hermex",
                                "Klintek",
                                "Ultracraft" };
            cmbMarca.ItemsSource = marca;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Articulo a = new Articulo();
            Marca m = new Marca();
            ControladorArticulo CA = new ControladorArticulo();
            if(cmbMarca.SelectedIndex != 0)
            {
                m.Id = cmbMarca.SelectedIndex;
                a.Caja = Int32.Parse(txtCaja.Text);
                a.Clave = txtClave.Text;
                a.Codigo = txtCodigo.Text;
                a.Descripcion = txtDescripcion.Text;
                a.Precio = Double.Parse(txtPrecio.Text);
                a.Marca = m;
                CA.AddArticulo(a);

            }
            
            Close();
        }
    }
}
