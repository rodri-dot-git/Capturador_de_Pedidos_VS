using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Capturador_de_pedidos.Controlador
{
    class ControladorUserControl
    {
        public void CargarUCGrid(Grid g, UserControl uc) {
			if (g.Children.Count != 0) {
				g.Children.Clear();
				g.Children.Add(uc);
			}
			else
			{
				g.Children.Add(uc);
			}
		}
    }
}
