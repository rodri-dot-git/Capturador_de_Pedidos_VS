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
    class ControladorUsuario
    {
        public Usuario Login(String nick, String pwd)
        {
            Usuario _usuario = new Usuario();
            Conexion conn = new Conexion();
            DataTableReader reader = null;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                conn.Conectar();
                _Parametros.Add(new SqlParameter("@Usuario", nick));
                _Parametros.Add(new SqlParameter("@Password", pwd));
                conn.PrepararProcedimiento("dbo.sp_Login", _Parametros);
                reader = conn.EjecutarTableReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    _usuario.Id = Int32.Parse(reader["IdUsuario"].ToString());
                    _usuario.Nombre = reader["Nombre"].ToString();
                    _usuario.Apellido = reader["Apellido"].ToString();
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("Datos incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            return _usuario;
        }
    }
}
