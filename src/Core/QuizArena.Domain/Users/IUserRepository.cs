using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Repositories;

namespace QuizArena.Domain.Users;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}