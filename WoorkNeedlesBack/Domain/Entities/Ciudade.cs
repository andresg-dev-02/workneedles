using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ciudade
    {
        public int Id { get; private set; }
        public int Iddepart { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public DateTime? Fechacreacion { get; private set; }
        public string DepartamentoNombre { get; private set; } = string.Empty;
        public string PaisNombre { get; private set; } = string.Empty;
        
        private Ciudade() { }
    }
}