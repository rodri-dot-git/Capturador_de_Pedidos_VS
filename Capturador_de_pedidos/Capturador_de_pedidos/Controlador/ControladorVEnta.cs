using Capturador_de_pedidos.BD;
using Capturador_de_pedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capturador_de_pedidos.Controlador
{
    class ControladorVenta
    {
        public void InsertarDatos(int user, int cliente, ObservableCollection<DetalleVenta> detalles)
        {
            Conexion conn = new Conexion();
            DataTable dt = new DataTable();
            dt.Columns.Add("IdArticulo", typeof(int));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Precio", typeof(double));
            DataTable arts = dt;
            DataRow dr = dt.NewRow();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            conn.Conectar();
            
            for (int i = 0; i < detalles.Count; i++)
            {
                arts.Rows.Add(detalles.ElementAt(i).Articulo.Id, detalles.ElementAt(i).Cantidad, detalles.ElementAt(i).PrecioVenta);
            }
            
            _Parametros.Add(new SqlParameter("@User", user));
            _Parametros.Add(new SqlParameter("@Cliente", cliente));
            _Parametros.Add(new SqlParameter("@Entrada", arts));
            conn.PrepararProcedimiento("dbo.sp_InsertVenta", _Parametros);
            conn.EjecutarProcedimiento();
            conn.Desconectar();
        }
    }
}
