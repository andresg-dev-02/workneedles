using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Ports.Output.Email
{
    public interface IEmailService
    {
        Task EnviarCorreoEnvioAsync(string emailCliente, string nombreCliente, string apellidoCliente,
            int idPedido, string direccion, DateOnly fechaEstimada, string tokenConfirmacion);
        
        Task EnviarCorreoConfirmacionAsync(string emailCliente, string nombreCliente, string apellidoCliente, int idPedido);
    }
}