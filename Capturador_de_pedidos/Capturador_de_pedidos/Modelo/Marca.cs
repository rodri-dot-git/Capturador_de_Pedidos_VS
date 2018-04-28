using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capturador_de_pedidos.Modelo
{
    class Marca
    {
        private int id;
        private String nombre;
        public Marca() { }
        public Marca(int id, string marca)
        {
            Id = id;
            Nombre = marca;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
