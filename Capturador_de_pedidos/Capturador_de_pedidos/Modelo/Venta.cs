using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capturador_de_pedidos.Modelo
{
    class Venta
    {
        private int idCliente;
        private int idUsuario;
        private double subTotal;
        private double iVA;
        private double total;
        private int folio;
        private string date;
        private Cliente c;

        public int IdCliente { get => idCliente; set => idCliente = value; }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public double SubTotal { get => subTotal; set => subTotal = value; }
        public double IVA { get => iVA; set => iVA = value; }
        public double Total { get => total; set => total = value; }
        public int Folio { get => folio; set => folio = value; }
        public string Date { get => date; set => date = value; }
        public Cliente C { get => c; set => c = value; }

        public Venta() { }
        public Venta(int cl, int us)
        {
            IdCliente = cl;
            IdUsuario = us;
        }
    }
}
