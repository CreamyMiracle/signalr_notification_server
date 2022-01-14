using NotificationServer;
using NotificationServer.Hubs;
using NotificationServer.Models;

var builder = WebApplication.CreateBuilder(args);

// POI 1: SignalR
builder.Services.AddSignalR();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

Dictionary<string, List<Payload>> notifications = new Dictionary<string, List<Payload>>();
builder.Services.AddSingleton<Dictionary<string, List<Payload>>>(notifications);

var app = builder.Build();

// POI 3: Register MessageHub
app.MapHub<NotificationHub>(Constants.NotificationHub);

app.UseAuthorization();

app.MapControllers();

app.Run();
