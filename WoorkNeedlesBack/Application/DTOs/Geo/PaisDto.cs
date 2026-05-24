using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Geo
{
    public class PaisDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Codigo { get; set; } = string.Empty;

        public DateTime? Fechacreacion { get; set; }
    }

    public class DepartamentoDto
    {
        public int Id { get; set; }

        public int Idpais { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public DateTime? Fechacreacion { get; set; }

        public PaisDto Pais { get; set; } = null!;
    }

    public class CiudadDto
    {
        public int Id { get; set; }

        public int Iddepart { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string DepartamentoNombre { get; set; } = string.Empty;

        public string PaisNombre { get; set; } = string.Empty;

        public DateTime? Fechacreacion { get; set; }
    }
}