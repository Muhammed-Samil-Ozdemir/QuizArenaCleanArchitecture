using Microsoft.Extensions.DependencyInjection;

namespace QuizArena.Presentation;

public static class PresentationRegistrar
{
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers()
                .AddApplicationPart(typeof(PresentationRegistrar).Assembly);
            return services;
        }
}