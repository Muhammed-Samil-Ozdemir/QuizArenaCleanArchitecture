using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizArena.Domain.QuestionOptions;
using QuizArena.Domain.Questions;
using QuizArena.Domain.RoomParticipants;
using QuizArena.Domain.Rooms;
using QuizArena.Domain.UnitOfWorks;
using QuizArena.Domain.UserAnswers;
using QuizArena.Domain.Users;
using QuizArena.Persistance.Context;
using QuizArena.Persistance.QuestionOptions;
using QuizArena.Persistance.Questions;
using QuizArena.Persistance.RoomParticipants;
using QuizArena.Persistance.Rooms;
using QuizArena.Persistance.UnitOfWorks;
using QuizArena.Persistance.UserAnswers;
using QuizArena.Persistance.Users;

namespace QuizArena.Persistance;

public static class PersistanceRegistrar
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {      
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IQuestionOptionRepository, QuestionOptionRepository>();
        services.AddScoped<IRoomParticipantRepository, RoomParticipantRepository>();
        services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}