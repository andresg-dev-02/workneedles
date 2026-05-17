using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Ports.Output
{
    public interface ITokenGenerator
    {
        string Generar(Usuario usuario);
    }
}