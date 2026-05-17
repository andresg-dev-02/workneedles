using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Ports;
using BC = BCrypt.Net.BCrypt;

namespace Infraestructure.Services
{
    public class PasswordHash : IPasswordHash
    {
        public string Hashear(string contrasena)
        {
            return BC.HashPassword(contrasena);
        }

        public bool Verificar(string contrasena, string hash)
        {
            return BC.Verify(contrasena, hash);
        }
    }
}