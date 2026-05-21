using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Cliente
    {
        public int Id { get; private set; }
        public int Idpais { get; private set; }
        public int Iddepart { get; private set; }
        public int Idciudad { get; private set; }
        public string Tipocliente { get; private set; } = string.Empty;
        public string Tipodocumento { get; private set; } = string.Empty;
        public string Documento { get; private set; } = string.Empty;
        public string? Nombres { get; private set; }
        public string? Apellidos { get; private set; }
        public string? Razonsocial { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string Telefono { get; private set; } = string.Empty;
        public string Direccion { get; private set; } = string.Empty;
        public string? PreferenciasCompra { get; private set; }
        public bool Activo { get; private set; }
        public DateTime? Fechacreacion { get; private set; }
        public DateTime? Fechamodificacion { get; private set; }
        public string Pais { get; private set; } = string.Empty;
        public string Departamento { get; private set; } = string.Empty;
        public string Ciudad { get; private set; } = string.Empty;

        private Cliente() { }

        public static Cliente Crear(int idPais, int idDepart, int idCiudad,
        string tipoCliente, string tipoDocumento, string documento,
        string? nombres, string? apellidos, string? razonSocial,
        string email, string telefono, string direccion,
        string? preferenciasCompra)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("El email es requerido.");
            if (!email.Contains('@'))
                throw new DomainException("El email no es válido.");
            if (string.IsNullOrWhiteSpace(documento))
                throw new DomainException("El documento es requerido.");
            if (string.IsNullOrWhiteSpace(telefono))
                throw new DomainException("El teléfono es requerido.");

            var c = new Cliente { Activo = true, Fechacreacion = DateTime.Now };
            c.Actualizar(idPais, idDepart, idCiudad, tipoCliente, tipoDocumento,
                documento, nombres, apellidos, razonSocial, email, telefono,
                direccion, preferenciasCompra);
            return c;
        }

        public void Actualizar(int idPais, int idDepart, int idCiudad,
            string tipoCliente, string tipoDocumento, string documento,
            string? nombres, string? apellidos, string? razonSocial,
            string email, string telefono, string direccion,
            string? preferenciasCompra)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("El email es requerido.");
            if (!email.Contains('@'))
                throw new DomainException("El email no es válido.");
            if (string.IsNullOrWhiteSpace(documento))
                throw new DomainException("El documento es requerido.");

            Idpais = idPais;
            Iddepart = idDepart;
            Idciudad = idCiudad;
            Tipocliente = tipoCliente.Trim();
            Tipodocumento = tipoDocumento.Trim();
            Documento = documento.Trim();
            Nombres = nombres?.Trim();
            Apellidos = apellidos?.Trim();
            Razonsocial = razonSocial?.Trim();
            Email = email.ToLowerInvariant();
            Telefono = telefono.Trim();
            Direccion = direccion.Trim();
            PreferenciasCompra = preferenciasCompra?.Trim();
            Fechamodificacion = DateTime.Now;
        }
    }
}