using AutoMapper;
using Application.Interfaces.User;
using Domain.Entities;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.UserRepo
{
    public class UserRepository(WoorkNeedlesContext context, IMapper mapper) : IUserRepository
    {
        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            var model = await context.Usuarios
                .AsNoTracking()
                .Include(u => u.IdrolNavigation)
                .Include(u => u.IdpaisNavigation)
                .Include(u => u.IdciudadNavigation)
                .FirstOrDefaultAsync(u => u.Email == email);
            return model is null ? null : mapper.Map<Usuario>(model);
        }
    }
}