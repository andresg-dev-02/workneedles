using Domain.Entities;
using Application.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Infraestructure.Mappings;
using AutoMapper;

namespace Infraestructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly WoorkNeedlesContext _context;
    private readonly IMapper _mapper;

    public UserRepository(WoorkNeedlesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Usuario> GetByEmailAsync(string email)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        return _mapper.Map<Usuario>(usuario);
    }

    public async Task<Usuario> GetByIdAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        return _mapper.Map<Usuario>(usuario);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return _mapper.Map<IEnumerable<Usuario>>(usuarios);
    }

    public async Task AddAsync(Usuario user)
    {
        var usuario = _mapper.Map<Infraestructure.Persistence.Models.Usuario>(user);
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario user)
    {
        var usuario = _mapper.Map<Infraestructure.Persistence.Models.Usuario>(user);
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Usuario user)
    {
        _context.Usuarios.Remove(_mapper.Map<Infraestructure.Persistence.Models.Usuario>(user));
        await _context.SaveChangesAsync();
    }
}