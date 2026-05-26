using Domain.Ports.Output.Email;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Infraestructure.Repositories.Email
{
    public class EmailRepository(IConfiguration config, HttpClient httpClient) : IEmailService
    {
        public async Task EnviarCorreoEnvioAsync(string emailCliente, string nombreCliente,
            string apellidoCliente, int idPedido, string direccion,
            DateOnly fechaEstimada, string tokenConfirmacion)
        {
            var urlConfirmacion = $"https://workneedles.onrender.com/api/Pedido/{idPedido}/confirmar-entrega?token={tokenConfirmacion}";

            var html = $"""
                <div style="font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 24px; border: 1px solid #eee; border-radius: 12px;">
                    <h2 style="color: #7B1D3F;">¡Tu pedido va en camino! 🚚</h2>
                    <p>Hola <strong>{nombreCliente} {apellidoCliente}</strong>,</p>
                    <p>Tu pedido <strong>#{idPedido}</strong> ha sido enviado a:</p>
                    <p style="background: #f9f9f9; padding: 12px; border-radius: 8px;">📍 {direccion}</p>
                    <p><strong>Fecha estimada de entrega:</strong> {fechaEstimada:dd/MM/yyyy}</p>
                    <a href="{urlConfirmacion}" style="display:inline-block;margin-top:16px;padding:12px 28px;background:#7B1D3F;color:white;text-decoration:none;border-radius:8px;font-weight:bold;">
                        ✅ Confirmar recepción
                    </a>
                </div>
            """;

            await EnviarAsync(emailCliente, $"Tu pedido #{idPedido} está en camino", html);
        }

        public async Task EnviarCorreoConfirmacionAsync(string emailCliente, string nombreCliente,
            string apellidoCliente, int idPedido)
        {
            var html = $"""
                <div style="font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 24px; border: 1px solid #eee; border-radius: 12px;">
                    <h2 style="color: #7B1D3F;">¡Entrega confirmada! ✅</h2>
                    <p>Hola <strong>{nombreCliente} {apellidoCliente}</strong>,</p>
                    <p>Hemos registrado que recibiste tu pedido <strong>#{idPedido}</strong>. ¡Gracias por confiar en WoorkNeedles!</p>
                </div>
            """;

            await EnviarAsync(emailCliente, $"Pedido #{idPedido} entregado — ¡Gracias!", html);
        }

        private async Task EnviarAsync(string destinatario, string asunto, string html)
        {
            var apiKey = config["Resend:ApiKey"];
            var remitente = config["Resend:From"]; 

            var payload = new
            {
                from = remitente,
                to = new[] { destinatario },
                subject = asunto,
                html
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.resend.com/emails");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            request.Content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}