using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capturador_de_pedidos.Controlador;
using Capturador_de_pedidos.Modelo;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
// Clase de metodos para la creacion de documento pdf de pedidos
namespace Capturador_de_pedidos.PDF
{
    class PDFMaker
    {
        public static void NewPedido(ObservableCollection<DetalleVenta> dv)
        {
            int tr = 0, ot = 0;
            for (int i = 0; i < dv.Count; i++)
            {
                if (dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Truper") || dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Pretul") || dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Fiero")) tr++;
                else ot++;
            }
            PdfPCell br = new PdfPCell(new Phrase(""));
            br.Colspan = 6;
            Image img = Image.GetInstance(@"Img\logo_ferretodo_nuevo.png");
            img.Alignment = Image.ALIGN_CENTER;
            img.ScaleToFit(570f, 200f);
            string folio;
            string clienteN;
            if (dv.ElementAt(0).Venta.Folio == 0) folio = GetFolio(dv.ElementAt(0).Venta.IdUsuario);
            else folio = GetFolio(dv.ElementAt(0).Venta.IdUsuario).Substring(0,1) + dv.ElementAt(0).Venta.Folio.ToString();
            if (dv.ElementAt(0).Venta.C == null) clienteN = GetCliente(dv.ElementAt(0).Venta.IdCliente);
            else clienteN = dv.ElementAt(0).Venta.C.Nombre;
            string vendedorN = GetVendedor(dv.ElementAt(0).Venta.IdUsuario);
            FileStream fs = new FileStream(@"PDF/" + "Pedido " + folio + ".pdf", FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            document.AddAuthor("Capturador de Pedidos");
            document.AddCreator("Caprutador de Pedidos por Rodrigo de León");
            document.AddSubject("Pedido");
            document.AddTitle("Pedido");
            PdfPTable table = new PdfPTable(6);
            table.TotalWidth = 580f;
            table.LockedWidth = true;
            float[] widths = new float[] { 60f, 60f, 60f, 280, 60f, 60f };
            table.SetWidths(widths);
            PdfPCell cliente = null;
            PdfPCell vendedor = null;
            PdfPCell empty = new PdfPCell(new Phrase());
            if (tr > 0)
            {
                document.Add(img);
                table.AddCell("Folio:");
                table.AddCell(folio);
                table.AddCell("Cliente:");
                cliente = new PdfPCell(new Phrase(clienteN));//CONSULTA SQL CLIENTE
                cliente.Colspan = 3;
                table.AddCell(cliente);
                table.AddCell("Vendedor:");
                vendedor = new PdfPCell(new Phrase(vendedorN));//CONSULTA SQL VENDEDOR
                vendedor.Colspan = 5;
                table.AddCell(vendedor);
                table.AddCell(br);
                table.AddCell("Marca");
                table.AddCell("Cantidad");
                table.AddCell("Codigo");
                table.AddCell("Descripción");
                table.AddCell("Precio/U");
                table.AddCell("SubTotal");
                for (int i = 0; i < dv.Count; i++)
                {
                    if (dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Truper") || dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Pretul") || dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Fiero"))
                    {
                        table.AddCell(dv.ElementAt(i).Articulo.Marca.Nombre);
                        table.AddCell(dv.ElementAt(i).Cantidad.ToString());
                        if (dv.ElementAt(i).Articulo.Codigo == null && dv.ElementAt(i).Articulo.Codigo.Equals("")) table.AddCell(dv.ElementAt(i).Articulo.Clave);
                        else table.AddCell(dv.ElementAt(i).Articulo.Codigo);
                        table.AddCell(dv.ElementAt(i).Articulo.Descripcion);
                        table.AddCell(dv.ElementAt(i).PrecioVenta.ToString());
                        table.AddCell((dv.ElementAt(i).PrecioVenta * dv.ElementAt(i).Cantidad).ToString());
                    }
                }
                empty.Colspan = 4;
                empty.Rowspan = 3;
                table.AddCell(empty);
                table.AddCell("SubTotal:");
                table.AddCell(Math.Round(dv.ElementAt(dv.Count - 1).Venta.SubTotal, 2).ToString());
                table.AddCell("I.V.A.:");
                table.AddCell(Math.Round(dv.ElementAt(dv.Count - 1).Venta.IVA, 2).ToString());
                table.AddCell("Total:");
                table.AddCell(Math.Round(dv.ElementAt(dv.Count - 1).Venta.Total, 2).ToString());
                document.Add(table);
                table = null;
            }
            if (ot > 0)
            {
                if (tr >= 20 || ot >= 20)
                {
                    document.NewPage();
                }
                table = new PdfPTable(6);
                table.TotalWidth = 580f;
                table.LockedWidth = true;
                table.SetWidths(widths);
                document.Add(img);
                table.AddCell("Folio:");
                table.AddCell(folio);
                table.AddCell("Cliente:");
                cliente = new PdfPCell(new Phrase(clienteN));//CONSULTA SQL CLIENTE
                cliente.Colspan = 3;
                table.AddCell(cliente);
                table.AddCell("Vendedor:");
                vendedor = new PdfPCell(new Phrase(vendedorN));//CONSULTA SQL VENDEDOR
                vendedor.Colspan = 5;
                table.AddCell(vendedor);
                table.AddCell(br);
                table.AddCell("Marca");
                table.AddCell("Cantidad");
                table.AddCell("Codigo");
                table.AddCell("Descripción");
                table.AddCell("Precio/U");
                table.AddCell("SubTotal");
                for (int i = 0; i < dv.Count; i++)
                {
                    if (dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Truper") || dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Pretul") || dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Fiero"))
                    {
                    }
                    else
                    {
                        table.AddCell(dv.ElementAt(i).Articulo.Marca.Nombre);
                        table.AddCell(dv.ElementAt(i).Cantidad.ToString());
                        if (dv.ElementAt(i).Articulo.Codigo == null && dv.ElementAt(i).Articulo.Codigo.Equals("")) table.AddCell(dv.ElementAt(i).Articulo.Clave);
                        else table.AddCell(dv.ElementAt(i).Articulo.Codigo);
                        table.AddCell(dv.ElementAt(i).Articulo.Descripcion);
                        table.AddCell(dv.ElementAt(i).PrecioVenta.ToString());
                        table.AddCell((dv.ElementAt(i).PrecioVenta * dv.ElementAt(i).Cantidad).ToString());
                    }
                }
                table.AddCell(empty);
                table.AddCell("SubTotal:");
                table.AddCell(Math.Round(dv.ElementAt(dv.Count - 1).Venta.SubTotal, 2).ToString());
                table.AddCell("I.V.A.:");
                table.AddCell(Math.Round(dv.ElementAt(dv.Count - 1).Venta.IVA, 2).ToString());
                table.AddCell("Total:");
                table.AddCell(Math.Round(dv.ElementAt(dv.Count - 1).Venta.Total, 2).ToString());
                document.Add(table);
            }
            document.Close();
            writer.Close();
            fs.Close();
        }
        public static string GetCliente(int id)
        {
            string c;
            string connS = ConfigurationManager.ConnectionStrings["Capturador_de_pedidos.Properties.Settings.DatabaseConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connS);
            List<SqlParameter> _iParametros = new List<SqlParameter>();
            String sqlConsulta = "SELECT Nombre FROM Cliente WHERE IdCliente = " + id;
            conn.Open();
            SqlCommand comando = new SqlCommand(sqlConsulta, conn);
            SqlDataReader reader = comando.ExecuteReader();
            reader.Read();
            c = reader["Nombre"].ToString();
            reader.Close();
            conn.Close();
            return c;
        }
        public static string GetVendedor(int id)
        {
            string c;
            string connS = ConfigurationManager.ConnectionStrings["Capturador_de_pedidos.Properties.Settings.DatabaseConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connS);
            List<SqlParameter> _iParametros = new List<SqlParameter>();
            String sqlConsulta = "SELECT CONCAT(Nombre, ' ', Apellido) 'Nombre' FROM Usuario WHERE IdUsuario = " + id;
            conn.Open();
            SqlCommand comando = new SqlCommand(sqlConsulta, conn);
            SqlDataReader reader = comando.ExecuteReader();
            reader.Read();
            c = reader["Nombre"].ToString();
            reader.Close();
            conn.Close();
            return c;
        }
        public static string GetFolio(int id)
        {
            string folio;
            string connS = ConfigurationManager.ConnectionStrings["Capturador_de_pedidos.Properties.Settings.DatabaseConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connS);
            List<SqlParameter> _iParametros = new List<SqlParameter>();
            String sqlConsulta = "SELECT TOP (1) CONCAT((SELECT LEFT(Nombre, 1) FROM Usuario WHERE IdUsuario = " + id + "), MAX(Folio)) AS 'Folio' FROM Venta ORDER BY Folio DESC";
            conn.Open();
            SqlCommand comando = new SqlCommand(sqlConsulta, conn);
            SqlDataReader reader = comando.ExecuteReader();
            reader.Read();
            folio = reader["Folio"].ToString();
            reader.Close();
            conn.Close();
            return folio;
        }
    }
}
