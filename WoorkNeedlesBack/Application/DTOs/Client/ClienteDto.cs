using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Client
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Tipocliente { get; set; } = string.Empty;
        public string Tipodocumento { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Razonsocial { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string? PreferenciasCompra { get; set; }
        public bool Activo { get; set; }
        public string Pais { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public DateTime? Fechamodificacion { get; set; }
    }

    public class CreateClienteDto
    {
        public int IdPais { get; set; }
        public int IdDepart { get; set; }
        public int IdCiudad { get; set; }
        public string Tipocliente { get; set; } = string.Empty;
        public string Tipodocumento { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Razonsocial { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string? PreferenciasCompra { get; set; }
    }

    public class UpdateClienteDto : CreateClienteDto { }
}