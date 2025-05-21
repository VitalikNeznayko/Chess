var builder = WebApplication.CreateBuilder(args);

// Додаємо SignalR
builder.Services.AddSignalR();

// Додаємо CORS, щоб дозволити підключення з інших джерел
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed(_ => true) // Дозволяє підключення з будь-якого джерела
               .AllowCredentials();
    });
});

var app = builder.Build();

// Вмикаємо CORS
app.UseCors("CorsPolicy");

// Маршрутизація SignalR хабу
app.MapHub<ChessGame.Hubs.ChessHub>("/chessHub");

app.Run();
