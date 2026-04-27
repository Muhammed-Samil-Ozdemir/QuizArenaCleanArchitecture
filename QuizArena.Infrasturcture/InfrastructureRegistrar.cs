using Microsoft.Extensions.DependencyInjection;
using QuizArena.Application.Security;
using QuizArena.Infrasturcture.Security;

namespace QuizArena.Infrasturcture;

public static class InfrastructureRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        return services;
    }
}