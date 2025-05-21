var builder = WebApplication.CreateBuilder(args);

// ������ SignalR
builder.Services.AddSignalR();

// ������ CORS, ��� ��������� ���������� � ����� ������
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed(_ => true) // �������� ���������� � ����-����� �������
               .AllowCredentials();
    });
});

var app = builder.Build();

// ������� CORS
app.UseCors("CorsPolicy");

// ������������� SignalR ����
app.MapHub<ChessGame.Hubs.ChessHub>("/chessHub");

app.Run();
