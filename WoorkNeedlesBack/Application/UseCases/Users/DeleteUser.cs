using Application.Interfaces.User;

namespace Application.UseCases.Users;

public class DeleteUser(IUserRepository userRepository)
{
    public async Task Execute(int id)
    {
        await userRepository.DeleteAsync(id);
    }
}