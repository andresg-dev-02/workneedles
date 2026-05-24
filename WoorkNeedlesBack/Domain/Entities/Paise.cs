using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Paise
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = null!;
        public string Codigo { get; private set; } = null!;
        public DateTime? Fechacreacion { get; private set; }
    }
}