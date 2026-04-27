using Microsoft.EntityFrameworkCore;
using QuizArena.Domain.Users;
using QuizArena.Persistance.Context;
using QuizArena.Persistance.Repositories;

namespace QuizArena.Persistance.Users;

public sealed class UserRepository(AppDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
{
    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken) =>
        GetWhere(u => u.Username == username)
            .FirstOrDefaultAsync(cancellationToken);

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
        GetWhere(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
}