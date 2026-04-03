using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QuizArena.Application.Behaviors;

namespace QuizArena.Application;

public static class ApplicationRegistrar 
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistrar).Assembly));
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        return services;
    }
}