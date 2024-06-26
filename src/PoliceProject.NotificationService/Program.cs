using PoliceProject.NotificationService.Models;
using RabbitMQ.Client;

namespace PoliceProject.NotificationService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.AddServiceDefaults();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddGrpc();
        var notifConfigString = builder.Configuration.GetConnectionString("MessageBroker") ?? throw new ArgumentNullException("Message broker connection is not defined");

        builder.AddRabbitMQClient(notifConfigString, configureConnectionFactory: factory =>
        {
            ((ConnectionFactory)factory).DispatchConsumersAsync = true;
        });

        builder.Services.AddScoped<ServerSentNotificationEmitter>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapDefaultEndpoints();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGrpcService<Grpc.NotificationServiceImpl>();
        app.MapControllers();

        app.Run();
    }
}
