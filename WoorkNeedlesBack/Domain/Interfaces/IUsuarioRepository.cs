using Domain.Entities;

namespace Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> ListarAsync();

    Task<Usuario?> ObtenerPorEmailAsync(string email);

    Task AgregarAsync(Usuario usuario);

    Task ActualizarAsync();

    Task EliminarAsync(int id);
}
