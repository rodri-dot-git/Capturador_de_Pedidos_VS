using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capturador_de_pedidos.Modelo
{
    class DetalleVenta
    {
        private Venta venta;
        private Articulo articulo;
        private int cantidad;
        private double precioVenta;
        private double subTotal;

        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double PrecioVenta { get => precioVenta; set => precioVenta = value; }
        internal Venta Venta { get => venta; set => venta = value; }
        internal Articulo Articulo { get => articulo; set => articulo = value; }
        public double SubTotal { get => subTotal; set => subTotal = value; }

        public DetalleVenta() { }
    }
}
