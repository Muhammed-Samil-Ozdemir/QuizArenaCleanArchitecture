using QuizArena.Application;
using QuizArena.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

builder.Services.AddPersistance(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();