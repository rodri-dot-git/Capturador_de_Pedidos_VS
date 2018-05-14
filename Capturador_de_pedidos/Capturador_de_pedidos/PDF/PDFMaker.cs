using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Capturador_de_pedidos.PDF
{
    class PDFMaker
    {
        public static void NewPedido(ObservableCollection<DetalleVenta> dv)
        {
            PdfPCell br = new PdfPCell(new Phrase(""));
            br.Colspan = 6;
            Image img = Image.GetInstance(@"C:\Users\Rodrigo\source\repos\Capturador_de_Pedidos_VS\Capturador_de_pedidos\Capturador_de_pedidos\Img\logo_ferretodo_nuevo.png");
            img.Alignment = Image.ALIGN_CENTER;
            img.ScaleToFit(570f, 200f);
            string folio = GetFolio();
            string clienteN = GetCliente(dv.ElementAt(0).Venta.IdCliente);
            string vendedorN = GetVendedor(dv.ElementAt(0).Venta.IdUsuario);
            FileStream fs = new FileStream("C:\\Users\\Rodrigo\\Desktop" + "\\" + folio + ".pdf", FileMode.Create);
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
            float[] widths = new float[] { 60f, 60f, 60f, 280, 60f, 60f};
            table.SetWidths(widths);
            document.Add(img);
            table.AddCell("Folio:");
            table.AddCell(folio);
            table.AddCell("Cliente:");
            PdfPCell cliente = new PdfPCell(new Phrase(clienteN));//CONSULTA SQL CLIENTE
            cliente.Colspan = 3;
            table.AddCell(cliente);
            table.AddCell("Vendedor:");
            PdfPCell vendedor = new PdfPCell(new Phrase(vendedorN));//CONSULTA SQL VENDEDOR
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
                if(dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Truper") || dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Pretul") || dv.ElementAt(i).Articulo.Marca.Nombre.Equals("Fiero"))
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
            PdfPCell empty = new PdfPCell(new Phrase());
            empty.Colspan = 4;
            empty.Rowspan = 3;
            table.AddCell(empty);
            table.AddCell("SubTotal:");
            table.AddCell(dv.ElementAt(dv.Count-1).Venta.SubTotal.ToString());
            table.AddCell("I.V.A.:");
            table.AddCell(dv.ElementAt(dv.Count-1).Venta.IVA.ToString());
            table.AddCell("Total:");
            table.AddCell(dv.ElementAt(dv.Count-1).Venta.Total.ToString());
            document.Add(table);
            document.NewPage();
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
            table.AddCell(dv.ElementAt(dv.Count - 1).Venta.SubTotal.ToString());
            table.AddCell("I.V.A.:");
            table.AddCell(dv.ElementAt(dv.Count - 1).Venta.IVA.ToString());
            table.AddCell("Total:");
            table.AddCell(dv.ElementAt(dv.Count - 1).Venta.Total.ToString());
            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();
        }
        public static string GetCliente(int id)
        {
            string c;
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\BD\\Database.mdf;Integrated Security = True");
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
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\BD\\Database.mdf;Integrated Security = True");
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
        public static string GetFolio()
        {
            string folio;
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\BD\\Database.mdf;Integrated Security = True");
            List<SqlParameter> _iParametros = new List<SqlParameter>();
            String sqlConsulta = "SELECT TOP (1) CONCAT((SELECT LEFT(Nombre, 1) FROM Usuario), MAX(Folio)) AS 'Folio' FROM Venta ORDER BY Folio DESC";
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
