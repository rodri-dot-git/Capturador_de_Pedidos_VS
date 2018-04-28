using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Capturador_de_pedidos.BD;
using Capturador_de_pedidos.Modelo;

namespace Capturador_de_pedidos.Controlador
{
    class ControladorArticulo
    {
        Articulo objA = null;
        Marca objM = null;
        public Articulo getArticulo(string param)
        {
            objA = new Articulo();
            objM = new Marca();
            Conexion conn = new Conexion();
            DataTableReader reader = null;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            conn.Conectar();
            _Parametros.Add(new SqlParameter("@param", param));
            conn.PrepararProcedimiento("dbo.sp_SelectArticulo", _Parametros);
            reader = conn.EjecutarTableReader();
            if (reader.HasRows)
            {
                reader.Read();
                objA.Id = Int32.Parse(reader["A.Id"].ToString());
                objA.Codigo = reader["A.Codigo"].ToString();
                objA.Descripcion = reader["A.Descripcion"].ToString();
                objA.Precio = Double.Parse(reader["A.Precio"].ToString());
                objM.Id = Int32.Parse(reader["M.Id"].ToString());
                objM.Nombre = reader["M.Nombre"].ToString();
                objA.Marca = objM;
                reader.Close();
            }
            else
            {
                MessageBox.Show("Articulo no encontrado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return objA;
        }
    }
}
