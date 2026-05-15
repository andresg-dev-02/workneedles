using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly WoorkNeedlesContext _context;

    public UsuarioRepository(WoorkNeedlesContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> ListarAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario?> ObtenerPorEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AgregarAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}