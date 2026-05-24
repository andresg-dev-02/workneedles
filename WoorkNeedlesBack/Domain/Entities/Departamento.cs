using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Departamento
    {
        public int Id { get; private set; }
        public int Idpais { get; private set; }
        public string Nombre { get; private set; }
        public DateTime? Fechacreacion { get; private set; }
        public string? PaisNombre { get; private set; } = string.Empty;
    }
}