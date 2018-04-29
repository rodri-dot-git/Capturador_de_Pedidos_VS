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
        public int BuscaId(String param)
        {
            int id = 0;
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\BD\\Database.mdf;Integrated Security = True");
            List<SqlParameter> _iParametros = new List<SqlParameter>();
            String sqlConsulta = "SELECT IdCliente FROM Cliente WHERE Nombre LIKE '" + param + "');";
            conn.Open();
            SqlCommand comando = new SqlCommand(sqlConsulta, conn);
            SqlDataReader reader = comando.ExecuteReader();
            reader.Read();
            id = Int32.Parse(reader["IdCliente"].ToString());
            reader.Close();
            conn.Close();
            return id;
        }
        public List<String> GetAllClientes()
        {
            List<String> nombres = new List<String>();
            DataTableReader reader;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            Conexion conn = new Conexion();
            conn.Conectar();
            conn.PrepararProcedimiento("sp_GetAllClients", _Parametros);
            conn.EjecutarProcedimiento();
            reader = conn.EjecutarTableReader();
            while (reader.Read())
            {
                nombres.Add(reader["Nombre"].ToString());
            }
            return nombres;
        }
    }
}
