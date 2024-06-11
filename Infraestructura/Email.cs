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
            Rectangle pageSize = new Rectangle(80f * 72f / 25.4f, 297f * 72f / 25.4f); // 80 mm width, A4 height
            Document document = new Document(pageSize, 10f, 10f, 10f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            // Configurar fuentes
            Font fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            Font fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

            // Añadir la imagen
            string imagePath = "C:\\Users\\juant\\source\\repos\\VISTA\\Presentacion\\IMAGEN\\Logozon2.png";
            Image img = Image.GetInstance(imagePath);
            img.ScaleToFit(100f, 100f); // Ajusta el tamaño de la imagen
            img.Alignment = Element.ALIGN_CENTER;
            document.Add(img);

            // Añadir información de la empresa
            document.Add(new Paragraph("ALMACEN MOTO TALLER LA 4TA", fontBold) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("RUC: 49776788", fontNormal) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("CRA 4ta #17a-37", fontNormal) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("motorepuestoslacuarta@gmail.com", fontNormal) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("TELÉFONO: 3015855961", fontNormal) { Alignment = Element.ALIGN_CENTER });

            document.Add(new Paragraph("--------------------------------------------------------------", fontNormal) { Alignment = Element.ALIGN_CENTER });

            // Añadir información del cliente
            document.Add(new Paragraph($"CLIENTE: {venta.cliente.NombreCliente}", fontBold));
            document.Add(new Paragraph($"CC: {venta.cliente.Documento}", fontNormal)); // Assumed to be ID Venta for simplicity

            document.Add(new Paragraph("--------------------------------------------------------------", fontNormal) { Alignment = Element.ALIGN_CENTER });

            // Añadir detalles del comprobante
            document.Add(new Paragraph($"COMPROBANTE ELECTRÓNICO DE VENTA", fontBold));
            document.Add(new Paragraph($"No.: {venta.idVenta}", fontNormal)); // Assumed formatting
            document.Add(new Paragraph($"FECHA: {venta.FechaVenta.ToString("dd/MM/yyyy HH:mm:ss")}", fontNormal));
            document.Add(new Paragraph("--------------------------------------------------------------", fontNormal) { Alignment = Element.ALIGN_CENTER });

            // Añadir encabezados de productos
            document.Add(new Paragraph("CANT  PRECIO  DESCRIPCIÓN  SUBTOTAL", fontBold));
            document.Add(new Paragraph("--------------------------------------------------------------", fontNormal) { Alignment = Element.ALIGN_CENTER });

            PdfContentByte cb = writer.DirectContent;
            float yPosition = writer.GetVerticalPosition(true) - 20; // Initial vertical position

            foreach (var detalle in venta.detalles)
            {
                cb.BeginText();
                cb.SetFontAndSize(fontNormal.BaseFont, 10);
                cb.SetTextMatrix(document.Left, yPosition); // Position for cantidad
                cb.ShowText(detalle.cantidad.ToString());
                cb.SetTextMatrix(document.Left + 30, yPosition); // Position for precio
                cb.ShowText($"${detalle.precioVenta:F2}");
                cb.SetTextMatrix(document.Left + 80, yPosition); // Position for descripción
                cb.ShowText(detalle.producto.descripcion);
                cb.SetTextMatrix(document.Left + 180, yPosition); // Position for subtotal
                cb.ShowText($"${detalle.total:F2}");
                cb.EndText();
                yPosition -= 12; // Move to the next line
            }

            document.Add(new Paragraph("\n\n--------------------------------------------------------------", fontNormal) { Alignment = Element.ALIGN_CENTER });

            // Añadir totales
            document.Add(new Paragraph($"\nMONTO TOTAL: ${venta.montoTotal:F2}", fontBold));
            document.Add(new Paragraph($"MONTO PAGADO: ${venta.montoPago:F2}", fontBold));
            document.Add(new Paragraph($"MONTO CAMBIO: ${venta.montoCambio:F2}", fontBold));
            document.Add(new Paragraph("GRACIAS POR COMPRAR", fontNormal) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("VUELVA PRONTO", fontNormal) { Alignment = Element.ALIGN_CENTER });

            writer.CloseStream = false;
            document.Close();
            stream.Position = 0; // Reinicia la posición del stream para leer desde el principio
            GuardarPDFEnArchivo(stream, $"C:\\Users\\juant\\OneDrive\\Escritorio\\IV-SEMESTRE\\FACTURAS\\FACTURA{venta.idVenta}.pdf");
            return stream;

        }

        public void GuardarPDFEnArchivo(MemoryStream pdfStream, string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                pdfStream.CopyTo(file);
            }
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
