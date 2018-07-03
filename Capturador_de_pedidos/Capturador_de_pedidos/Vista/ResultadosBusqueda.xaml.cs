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
    /// Lógica de interacción para ResultadosBusqueda.xaml
    /// </summary>
    public partial class ResultadosBusqueda : Window
    {
        public ResultadosBusqueda(int folio)
        {
            InitializeComponent();
            ControladorUserControl UC = new ControladorUserControl();
            UC.CargarUCGrid(grdPrincipal, new UCFolio(folio));
        }
    }
}
