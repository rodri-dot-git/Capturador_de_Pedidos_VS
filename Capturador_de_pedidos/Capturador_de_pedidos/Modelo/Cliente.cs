using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capturador_de_pedidos.Modelo
{
    class Cliente
    {
        private int id;
        private String nombre;
        public Cliente() { }
        public Cliente(String n)
        {
            Nombre = n;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
