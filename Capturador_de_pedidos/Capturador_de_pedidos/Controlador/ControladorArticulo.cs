using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using Capturador_de_pedidos.BD;
using Capturador_de_pedidos.Modelo;

namespace Capturador_de_pedidos.Controlador
{
    class ControladorArticulo
    {
        Articulo objA = null;
        Marca objM = null;

        public void AddArticulo(Articulo a)
        {
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            Conexion conn = new Conexion();
            conn.Conectar();
            _Parametros.Add(new SqlParameter("@marca", a.Marca.Id));
            _Parametros.Add(new SqlParameter("@codigo", a.Codigo));
            _Parametros.Add(new SqlParameter("@clave", a.Clave));
            _Parametros.Add(new SqlParameter("@desc", a.Descripcion));
            _Parametros.Add(new SqlParameter("@precio", a.Precio));
            _Parametros.Add(new SqlParameter("@caja", a.Caja));
            conn.PrepararProcedimiento("sp_InsertArticulo", _Parametros);
            try
            {
                conn.EjecutarProcedimiento();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "Error");
            }
            catch { }
            
            MessageBox.Show("Se agrego articulo", "Articulo" , MessageBoxButton.OK);
        }
        public Articulo getArticulo(string param)
        {
            objA = new Articulo();
            objM = new Marca();
            Conexion conn = new Conexion();
            DataTableReader reader;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            conn.Conectar();
            _Parametros.Add(new SqlParameter("@param", param));
            conn.PrepararProcedimiento("dbo.sp_SelectArticulo", _Parametros);
            reader = conn.EjecutarTableReader();
            if (reader.HasRows)
            {
                reader.Read();
                objA.Id = Int32.Parse(reader["IdArticulo"].ToString());
                objA.Codigo = reader["Codigo"].ToString();
                objA.Descripcion = reader["Descripcion"].ToString();
                objA.Precio = Double.Parse(reader["Precio"].ToString());
                objM.Id = Int32.Parse(reader["IdMarca"].ToString());
                objM.Nombre = reader["Nombre"].ToString();
                objA.Marca = objM;
                reader.Close();
            }
            else
            {
                objA = null;
                MessageBox.Show("Articulo no encontrado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return objA;
        }
        public string getArticuloLike(string param)
        {
            objA = new Articulo();
            objM = new Marca();
            Conexion conn = new Conexion();
            DataTableReader reader;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            conn.Conectar();
            _Parametros.Add(new SqlParameter("@param", param));
            conn.PrepararProcedimiento("dbo.sp_SelectArticuloLike", _Parametros);
            reader = conn.EjecutarTableReader();
            if (reader.HasRows)
            {
                reader.Read();
                objA.Descripcion = reader["Descripcion"].ToString();
                reader.Close();
            }
            else
            {
                objA.Descripcion = "No encontrado";
            }

            return objA.Descripcion;
        }
    }
}
