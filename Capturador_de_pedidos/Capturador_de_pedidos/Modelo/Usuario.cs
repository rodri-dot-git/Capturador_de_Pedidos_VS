using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capturador_de_pedidos.Modelo
{
    class Usuario
    {
        private int Id { get; set; }
        private String Username { get; set; }
        private String Password { get; set; }
        private String Nombre { get; set; }
        private String Apellidos { get; set; }
        private int Status { get; set; }

        public Usuario(String a, String b, String c, String d, int e)
        {
            Username = a;
            Password = b;
            Nombre = c;
            Apellidos = d;
            Status = e;
        }
        public Usuario() { }
    }
}
