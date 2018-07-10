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
using System.Windows;

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
            try
            {
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
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                conn.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                conn.Desconectar();
            }
            finally
            {
                conn.Desconectar();
            }
            
        }
        public ObservableCollection<DetalleVenta> BuscarPorFolio(int folio)
        {
            ObservableCollection<DetalleVenta> ventas = new ObservableCollection<DetalleVenta>();
            DataTableReader reader;
            Venta v = null;
            DetalleVenta dv = null;
            Articulo a = null;
            Marca m = null;
            Cliente c = null;
            Conexion con = null;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            con = new Conexion();
            try
            {
                con.Conectar();
                _Parametros.Add(new SqlParameter("@folio", folio));
                con.PrepararProcedimiento("sp_BusquedaFolio", _Parametros);
                reader = con.EjecutarTableReader();
                while (reader.Read())
                {
                    v = new Venta();
                    dv = new DetalleVenta();
                    a = new Articulo();
                    c = new Cliente();
                    m = new Marca();
                    v.Folio = Int32.Parse(reader["Folio"].ToString());
                    a.Codigo = reader["Codigo"].ToString();
                    a.Descripcion = reader["Descripcion"].ToString();
                    a.Precio = Double.Parse(reader["PrecioVenta"].ToString());
                    m.Nombre = reader["Nombre"].ToString();
                    a.Marca = m;
                    dv.Articulo = a;
                    dv.Cantidad = Int32.Parse(reader["Cantidad"].ToString());
                    dv.PrecioVenta = a.Precio;
                    v.SubTotal = Double.Parse(reader["SubTotal"].ToString());
                    v.IVA = Double.Parse(reader["IVA"].ToString());
                    v.Total = Double.Parse(reader["Total"].ToString());
                    v.Date = reader["Fecha"].ToString();
                    dv.SubTotal = dv.PrecioVenta * dv.Cantidad;
                    c.Nombre = reader["Cliente"].ToString();
                    v.C = c;
                    v.IdUsuario = Int32.Parse(reader["IdUsuario"].ToString());
                    dv.Venta = v;
                    ventas.Add(dv);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                con.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                con.Desconectar();
            }
            finally
            {
                con.Desconectar();
            }
            return ventas;
        }
        public ObservableCollection<Venta> BuscarPorFecha(DateTime d1, DateTime d2)
        {
            ObservableCollection<Venta> ventas = new ObservableCollection<Venta>();
            DataTableReader reader;
            Venta v = null;
            Cliente c = null;
            Conexion con = null;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            con = new Conexion();
            try
            {
                con.Conectar();
                _Parametros.Add(new SqlParameter("@d1", SqlDbType.DateTime) { Value = d1 });
                _Parametros.Add(new SqlParameter("@d2", SqlDbType.DateTime) { Value = d2 });
                con.PrepararProcedimiento("sp_BusquedaFecha", _Parametros);
                reader = con.EjecutarTableReader();
                while (reader.Read())
                {
                    v = new Venta();
                    c = new Cliente();
                    v.Folio = Int32.Parse(reader["Id"].ToString());
                    c.Nombre = reader["Nombre"].ToString();
                    v.Total = Double.Parse(reader["Total"].ToString());
                    v.C = c;
                    v.Date = reader["fecha"].ToString();
                    ventas.Add(v);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                con.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                con.Desconectar();
            }
            finally
            {
                con.Desconectar();
            }
            return ventas;
        }
        public ObservableCollection<Venta> BuscarPorNombre(string nombre)
        {
            ObservableCollection<Venta> ventas = new ObservableCollection<Venta>();
            DataTableReader reader;
            Venta v = null;
            Cliente c = null;
            Conexion con = null;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            con = new Conexion();
            try
            {
                con.Conectar();
                _Parametros.Add(new SqlParameter("@nombre", nombre));
                con.PrepararProcedimiento("sp_BusquedaCliente", _Parametros);
                reader = con.EjecutarTableReader();
                while (reader.Read())
                {
                    v = new Venta();
                    c = new Cliente();
                    v.Folio = Int32.Parse(reader["Id"].ToString());
                    c.Nombre = reader["Nombre"].ToString();
                    v.Total = Double.Parse(reader["Total"].ToString());
                    v.C = c;
                    v.Date = reader["fecha"].ToString();
                    ventas.Add(v);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                con.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                con.Desconectar();
            }
            finally
            {
                con.Desconectar();
            }
            return ventas;
        }
        public ObservableCollection<Venta> BuscarPorNombreFecha(string name, DateTime d1, DateTime d2)
        {
            ObservableCollection<Venta> ventas = new ObservableCollection<Venta>();
            DataTableReader reader;
            Venta v = null;
            Cliente c = null;
            Conexion con = null;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            con = new Conexion();
            try
            {
                con.Conectar();
                _Parametros.Add(new SqlParameter("@nombre", name));
                _Parametros.Add(new SqlParameter("@d1", SqlDbType.DateTime) { Value = d1 });
                _Parametros.Add(new SqlParameter("@d2", SqlDbType.DateTime) { Value = d2 });
                con.PrepararProcedimiento("sp_BusquedaNombreFecha", _Parametros);
                reader = con.EjecutarTableReader();
                while (reader.Read())
                {
                    v = new Venta();
                    c = new Cliente();
                    v.Folio = Int32.Parse(reader["Id"].ToString());
                    c.Nombre = reader["Nombre"].ToString();
                    v.Total = Double.Parse(reader["Total"].ToString());
                    v.C = c;
                    v.Date = reader["fecha"].ToString();
                    ventas.Add(v);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                con.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
                con.Desconectar();
            }
            finally
            {
                con.Desconectar();
            }
            return ventas;
        }
    }
}
