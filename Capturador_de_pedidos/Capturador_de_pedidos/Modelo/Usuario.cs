using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capturador_de_pedidos.Modelo
{
    class Usuario
    {
        private int id;
        private String username;
        private String password;
        private String nombre;
        private String apellido;
        private int status;

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public int Status { get => status; set => status = value; }

        public Usuario(String a, String b, String c, String d, int e, int f)
        {
            Id = f;
            Username = a;
            Password = b;
            Nombre = c;
            Apellido = d;
            Status = e;
        }
        public Usuario() { }
    }
}
