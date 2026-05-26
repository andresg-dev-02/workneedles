using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Ports.Output.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infraestructure.Repositories.Email
{
    public class EmailRepository(IConfiguration config) : IEmailService
    {
        public async Task EnviarCorreoEnvioAsync(string emailCliente, string nombreCliente, string apellidoCliente,
            int idPedido, string direccion, DateOnly fechaEstimada, string TokenConfirmacion)
        {
            var urlConfirmacion = $"https://workneedles.onrender.com/api/Pedido/{idPedido}/confirmar-entrega?token={TokenConfirmacion }";

            var html = $"""
                <div style="font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 24px; border: 1px solid #eee; border-radius: 12px;">
                    <h2 style="color: #7B1D3F;">¡Tu pedido va en camino! 🚚</h2>
                    <p>Hola <strong>{nombreCliente} {apellidoCliente}</strong>,</p>
                    <p>Tu pedido <strong>#{idPedido}</strong> ha sido enviado y pronto llegará a tu dirección:</p>
                    <p style="background: #f9f9f9; padding: 12px; border-radius: 8px; color: #333;">
                        📍 {direccion}
                    </p>
                    <p><strong>Fecha estimada de entrega:</strong> {fechaEstimada:dd/MM/yyyy}</p>
                    <p>Cuando recibas tu pedido, confírmalo haciendo click en el botón:</p>
                    <a href="{urlConfirmacion}"
                       style="display: inline-block; margin-top: 16px; padding: 12px 28px;
                              background-color: #7B1D3F; color: white; text-decoration: none;
                              border-radius: 8px; font-weight: bold;">
                        ✅ Confirmar recepción
                    </a>
                    <p style="margin-top: 24px; font-size: 12px; color: #999;">
                        Si no realizaste este pedido, ignora este correo.
                    </p>
                </div>
            """;

            await EnviarAsync(emailCliente, $"Tu pedido #{idPedido} está en camino", html);
        }

        public async Task EnviarCorreoConfirmacionAsync(string emailCliente, string nombreCliente, string apellidoCliente, int idPedido)
        {
            var html = $"""
                <div style="font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 24px; border: 1px solid #eee; border-radius: 12px;">
                    <h2 style="color: #7B1D3F;">¡Entrega confirmada! ✅</h2>
                    <p>Hola <strong>{nombreCliente} {apellidoCliente}</strong>,</p>
                    <p>Hemos registrado que recibiste tu pedido <strong>#{idPedido}</strong>.</p>
                    <p>Gracias por confiar en <strong>WoorkNeedles</strong>. ¡Esperamos verte pronto!</p>
                </div>
            """;

            await EnviarAsync(emailCliente, $"Pedido #{idPedido} entregado — ¡Gracias!", html);
        }

        private async Task EnviarAsync(string destinatario, string asunto, string html)
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress(
                config["Email:NombreRemitente"],
                config["Email:Usuario"]));
            mensaje.To.Add(MailboxAddress.Parse(destinatario));
            mensaje.Subject = asunto;
            mensaje.Body = new TextPart("html") { Text = html };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(config["Email:Host"],
                int.Parse(config["Email:Port"]!),
                SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(config["Email:Usuario"], config["Email:Contrasena"]);
            await smtp.SendAsync(mensaje);
            await smtp.DisconnectAsync(true);
        }
    }
}