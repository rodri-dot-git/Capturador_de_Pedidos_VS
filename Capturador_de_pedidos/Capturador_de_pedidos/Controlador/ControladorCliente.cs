using Capturador_de_pedidos.BD;
using Capturador_de_pedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public void AddCliente(string c)
        {
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            Conexion conn = new Conexion();
            try
            {
                conn.Conectar();
                _Parametros.Add(new SqlParameter("@nom", c));
                conn.PrepararProcedimiento("sp_InsertCliente", _Parametros);
                conn.EjecutarProcedimiento();
                MessageBox.Show("Se agrego a: " + c, "Cliente", MessageBoxButton.OK);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            finally
            {
                conn.Desconectar();
            }
        }
        public int BuscaId(String param)
        {
            int id = 0;
            string connS = ConfigurationManager.ConnectionStrings["Capturador_de_pedidos.Properties.Settings.DatabaseConnectionString"].ToString();
            try
            {
                SqlConnection conn = new SqlConnection(connS);
                List<SqlParameter> _iParametros = new List<SqlParameter>();
                String sqlConsulta = "SELECT IdCliente FROM Cliente WHERE Nombre = '" + param + "'";
                conn.Open();
                SqlCommand comando = new SqlCommand(sqlConsulta, conn);
                SqlDataReader reader = comando.ExecuteReader();
                reader.Read();
                id = Int32.Parse(reader["IdCliente"].ToString());
                reader.Close();
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            return id;
        }
        public List<String> GetAllClientes()
        {
            List<String> nombres = new List<String>();
            DataTableReader reader;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            Conexion conn = new Conexion();
            try
            {
                conn.Conectar();
                conn.PrepararProcedimiento("sp_GetAllClients", _Parametros);
                conn.EjecutarProcedimiento();
                reader = conn.EjecutarTableReader();
                while (reader.Read())
                {
                    nombres.Add(reader["Nombre"].ToString());
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            finally
            {
                conn.Desconectar();
            }
            return nombres;
        }
    }
}
