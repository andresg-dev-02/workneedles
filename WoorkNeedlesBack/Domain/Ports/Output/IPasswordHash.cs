using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Ports.Output
{
    public interface IPasswordHash
    {
        string Hashear(string contrasena);
        bool Verificar(string contrasena, string hash);
    }
}