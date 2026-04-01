using QuizArena.Domain.Abstractions;

namespace QuizArena.Domain.Users;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsername(string username, CancellationToken cancellationToken);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
}