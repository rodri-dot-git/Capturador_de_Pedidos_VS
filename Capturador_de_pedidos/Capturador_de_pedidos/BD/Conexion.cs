using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Capturador_de_pedidos.BD
{
    class Conexion
    {
        private SqlConnection _conn = null;
        string _ConnectionString = ConfigurationManager.ConnectionStrings["Capturador_de_pedidos.Properties.Settings.DatabaseConnectionString"].ToString();
        bool _Conectado = false;

        string _NombreProcedimiento = "";
        List<SqlParameter> _Parametros = new List<SqlParameter>();
        bool _Preparado = false;
        SqlConnection conexion = new SqlConnection();
        public bool Conectar()
        {
            bool _Rsp = false;
            _conn = new SqlConnection(_ConnectionString);
            try
            {
                _conn.Open();
                _Conectado = true;
                _Rsp = true;
            }
            catch (SqlException SqlEx)
            {
                string MensajeError = "ERROR: " + SqlEx.Message + ". " + "LINEA: " + SqlEx.LineNumber + ".";
                throw new Exception(MensajeError, SqlEx);
            }
            catch
            {
                _Rsp = false;
            }
            return _Rsp;
        }
        public void Desconectar()
        {
            try
            {
                _conn.Close();
            }
            catch
            {

            }
        }
        public void PrepararProcedimiento(string NombreProcedimiento, List<SqlParameter> Parametros)
        {
            if (_Conectado)
            {
                _NombreProcedimiento = "";
                _Parametros.Clear();

                _NombreProcedimiento = NombreProcedimiento;
                _Parametros = Parametros;

                _Preparado = true;
            }
            else
            {
                throw new Exception("No hay conexion a base de datos");
            }
        }
        public DataTableReader EjecutarTableReader()
        {
            _Preparado = true;
            if (_Preparado)
            {
                DataTable dt = new DataTable();
                SqlCommand cmm = new SqlCommand(_NombreProcedimiento, _conn);
                cmm.CommandType = CommandType.StoredProcedure;
                cmm.Parameters.AddRange(_Parametros.ToArray());
                SqlDataAdapter adt = new SqlDataAdapter(cmm);
                adt.Fill(dt);
                _Preparado = false;
                cmm = null;
                return dt.CreateDataReader();
            }
            else
            {
                _Preparado = false;
                //throw new Exception("Procedimiento no preparado");
                return null;
            }
        }
        public int EjecutarProcedimiento()
        {
            if (_Preparado)
            {
                SqlCommand cmm = new SqlCommand(_NombreProcedimiento, _conn);
                cmm.CommandType = System.Data.CommandType.StoredProcedure;
                cmm.CommandTimeout = 120;
                cmm.Parameters.AddRange(_Parametros.ToArray());
                _Preparado = false;
                return cmm.ExecuteNonQuery();
            }
            else
            {
                _Preparado = false;
                throw new Exception("Procedimiento no preparado");
            }
        }
    }
}
