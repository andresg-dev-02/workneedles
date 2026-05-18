using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Ports.Output;
using Application.DTOs.Login;
using Application.Interfaces.User;
using Domain.Exceptions;

namespace Application.UseCases.Login
{
    public class Auth(IUserRepository userRepository, IPasswordHash passwordHash, ITokenGenerator tokenGenerator)
    {
        public async Task<TokenDto> Login(LoginDto usuarioLoginDto)
        {
            var usuario = await userRepository.GetByEmailAsync(usuarioLoginDto.Email)
                ?? throw new DomainException("Credenciales inválidas.");

            if (!passwordHash.Verificar(usuarioLoginDto.Contrasena, usuario.Contrasena))
                throw new DomainException("Credenciales inválidas.");

            if (!usuario.Activo)
                throw new DomainException("El usuario no está activo. \n Contacta al administrador.");

            var token = tokenGenerator.Generar(usuario);

            return new TokenDto
            {
                Token = token,
                Expiracion = DateTime.Now.AddHours(2)
            };
        }
    }
}