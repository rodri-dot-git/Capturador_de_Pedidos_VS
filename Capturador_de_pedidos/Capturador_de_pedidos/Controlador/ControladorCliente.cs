using Capturador_de_pedidos.BD;
using Capturador_de_pedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Capturador_de_pedidos.Controlador
{
    class ControladorCliente
    {
        public void AddCliente(Cliente c)
        {
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            Conexion conn = new Conexion();
            conn.Conectar();
            _Parametros.Add(new SqlParameter("@nom", c.Nombre));
            conn.PrepararProcedimiento("sp_InsertCliente", _Parametros);
            conn.EjecutarProcedimiento();

            MessageBox.Show("Se agrego a: " + c.Nombre, "Cliente", MessageBoxButton.OK);
        }
    }
}
