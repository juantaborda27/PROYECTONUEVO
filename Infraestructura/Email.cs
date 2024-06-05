using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using ENTIDADES;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace Infraestructura
{
    public class Email
    {
        private string myEmail = "josevidalquintero2021@gmail.com"; // Asegúrate de reemplazar esto con tu email
        private string MyPassword = "ghaj exiw prwp yrue"; // Asegúrate de reemplazar esto con tu contraseña
        private string MyAlias = "TVV";
        private MailMessage mCorreo;


        public void CrearCuerpoCorreo(string Emisor, Venta venta)
        {
            mCorreo = new MailMessage();
            mCorreo.From = new MailAddress(myEmail, MyAlias, System.Text.Encoding.UTF8);
            mCorreo.To.Add(Emisor);
            mCorreo.Subject = "COMPRA REALIZADA ALMACEN MOTO TALLER LA 4TA";
            mCorreo.Body = "En el adjunto encontrará el PDF con los detalles de su compra.";
            mCorreo.IsBodyHtml = true;
            mCorreo.Priority = MailPriority.High;

            // Generar PDF en memoria
            MemoryStream pdfStream = GenerarPDF(venta);
            pdfStream.Position = 0; // Asegúrate de restablecer la posición del stream

            // Crear el archivo adjunto
            Attachment attachment = new Attachment(pdfStream, "FacturaCompra.pdf", "application/pdf");
            mCorreo.Attachments.Add(attachment);
        }

        private MemoryStream GenerarPDF(Venta venta)
        {
            MemoryStream stream = new MemoryStream();
            Document document = new Document(PageSize.A4, 50, 50, 50, 50);
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();


            string imagePath = "C:/Users/juant/Downloads/EDICIONPROYECT/EDICIONPROYECT/PROYECTOEDICIONTODO/ProyectoRest/ProyectoRest/IMAGEN/Logazon.png";
            Image img = Image.GetInstance(imagePath);
            img.ScaleToFit(200f, 200f); // Ajusta el tamaño de la imagen
            img.SetAbsolutePosition(50, document.PageSize.Height - 100); // Posición en la parte superior izquierda
            document.Add(img);

            // Añadir título
            document.Add(new Paragraph(""));
            document.Add(new Paragraph(""));
            document.Add(new Paragraph(""));
            document.Add(new Paragraph(""));
            document.Add(new Paragraph(""));
            document.Add(new Paragraph("Factura de Venta"));
            document.Add(new Paragraph($"ID Venta: {venta.idVenta}"));
            document.Add(new Paragraph(""));
            //document.Add(new Paragraph($"Usuario: {venta.usuario.idUsuario}"));
            document.Add(new Paragraph($"Cliente: {venta.nombreCliente}"));
            document.Add(new Paragraph(""));
            document.Add(new Paragraph($"Fecha de Venta: {venta.FechaVenta.ToShortDateString()}"));
            document.Add(new Paragraph(""));
            document.Add(new Paragraph("")); // Espacio en blanco

            // Añadir detalles de la venta
            PdfPTable table = new PdfPTable(4);
            table.AddCell("Descripción del Producto");
            table.AddCell("Precio");
            table.AddCell("Cantidad");
            table.AddCell("SubTotal");

            foreach (var detalle in venta.detalles)
            {
                table.AddCell(detalle.producto.descripcion);
                table.AddCell("$"+detalle.precioVenta.ToString());
                table.AddCell(detalle.cantidad.ToString());
                table.AddCell("$"+detalle.total.ToString());
            }

            document.Add(table);

            // Añadir montos
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph($"Monto Total: ${venta.montoTotal.ToString()}"));
            document.Add(new Paragraph($"Monto Pagado: ${venta.montoPago.ToString()}"));
            document.Add(new Paragraph($"Monto Cambio: ${venta.montoCambio.ToString()}"));
            
            writer.CloseStream = false;
            document.Close();
            return stream;
        }

        public string Enviar(Venta venta, string correo)
        {
            CrearCuerpoCorreo(correo, venta);
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new NetworkCredential(myEmail, MyPassword);

                // Validación del certificado SSL (ten cuidado con esto en producción)
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                {
                    return true;
                };

                smtp.EnableSsl = true;
                smtp.Send(mCorreo);
                return "Enviado con Exito";
            }
            catch (Exception e)
            {
                return ("Error: " + e.Message);
            }
        }
    }
}
