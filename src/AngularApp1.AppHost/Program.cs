using Aspire.Hosting;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Resources;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("policeProjectSqlServer-password", secret: true);

var mssql = builder.AddSqlServer("policeProjectSqlServer", password, port: 29334)
    .WithDataVolume();

var identitydb = mssql.AddDatabase("identitydb");   
var notifdb = mssql.AddDatabase("notification");
var main = mssql.AddDatabase("main");
var hangfire = mssql.AddDatabase("hangfire");

var rabbitMq = builder.AddRabbitMQ("MessageBroker");

var identityApi = builder.AddProject<Projects.IdentityService>("policeproject-identityservice", "https")
    .WithExternalHttpEndpoints()
    .WithReference(identitydb);

var identityEndpoint = identityApi.GetEndpoint("https") ?? throw new ArgumentNullException("Failed to retreive endpoint");

var notificationServiceApi = builder.AddProject<Projects.NotificationService>("policeproject-notificationservice")
    .WithReference(rabbitMq)
    .WithReference(notifdb)
    .WithEnvironment("Identity__Url", identityEndpoint);

var mainApi = builder.AddProject<Projects.MainService>("policeproject-main")
    .WithReference(rabbitMq)
    .WithReference(hangfire)
    .WithReference(main)
    .WithEnvironment("Identity__Url", identityEndpoint)
    .WithEnvironment("Notification__Url", notificationServiceApi.GetEndpoint("https"));

var angularWebApp = builder.AddNpmApp("WebApp", "../angularapp1.client")
    .WithEnvironment("Identity__Url", identityEndpoint)
    .WithEnvironment("Main__Url", mainApi.GetEndpoint("https"))
    .WithEnvironment("Notification__Url", notificationServiceApi.GetEndpoint("https"))
    .PublishAsDockerFile();

identityApi
    .WithEnvironment("MainApiClient", mainApi.GetEndpoint("https"))
    .WithEnvironment("NotificationServiceClient", notificationServiceApi.GetEndpoint("https"));

builder.Build().Run();
