using Capturador_de_pedidos.BD;
using Capturador_de_pedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capturador_de_pedidos.Controlador
{
    class ControladorVenta
    {
        public void InsertarDatos(int user, int cliente, List<DetalleVenta> detalles)
        {
            Conexion conn = new Conexion();
            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            conn.Conectar();
            dt.Columns.Add("IdArticulo", typeof(int));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Precio", typeof(double));
            for (int i = 0; i < detalles.Count; i++)
            {
                dr[0] = detalles[i].Articulo.Id;
                dr[1] = detalles[i].Cantidad;
                dr[2] = detalles[i].PrecioVenta;
                dt.Rows.Add(dr);
            }
            
            _Parametros.Add(new SqlParameter("@User", user));
            _Parametros.Add(new SqlParameter("@Cliente", cliente));
            _Parametros.Add(new SqlParameter("@Entrada", dt));
            conn.PrepararProcedimiento("dbo.sp_InsertVenta", _Parametros);
        }
    }
}
