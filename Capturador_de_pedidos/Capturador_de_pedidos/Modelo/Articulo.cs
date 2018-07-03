using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capturador_de_pedidos.Modelo
{
    class Articulo
    {
        private int id;
        private String codigo;
        private String clave;
        private String descripcion;
        private double precio;
        private Marca marca;
        private int caja;
        public Articulo() { }
        public Articulo(int a, String b, String c, String d, double e, Marca f)
        {
            Id = a;
            Codigo = b;
            Clave = c;
            Descripcion = d;
            Precio = e;
            Marca = f;
        }

        public int Id { get => id; set => id = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Clave { get => clave; set => clave = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public double Precio { get => precio; set => precio = value; }
        public Marca Marca { get => marca; set => marca = value; }
        public int Caja { get => caja; set => caja = value; }
    }
}
