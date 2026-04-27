using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizArena.Persistance.Context;

namespace QuizArena.Persistance;

public static class PersistanceRegistrar
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {      
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        return services;
    }
}