using Microsoft.AspNetCore.Authentication.Negotiate;
using PoliceProject.IdentityService.Entities;
using PoliceProject.IdentityService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using AngularApp1.Server.Extensions;
using PoliceProject.IdentityService.Grpc;

namespace PoliceProject.IdentityService;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services.AddDbContext<PoliceProjectIdentityContext>(options =>
        {
            var identityConnectionString = builder.Configuration.GetConnectionString("identitydb");
            options.UseSqlServer(identityConnectionString);
        }, ServiceLifetime.Transient);

        builder.Services.AddIdentityApiEndpoints<User>()
                .AddRoles<Position>()
                .AddEntityFrameworkStores<PoliceProjectIdentityContext>();

        builder.Services.AddIdentityServer(options =>
        {
            options.UserInteraction.LoginUrl = "/login";
        })      
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryApiScopes(Config.GetScopes())
            .AddInMemoryClients(Config.GetClients(builder.Configuration))
            .AddInMemoryIdentityResources(Config.GetResources())
            .AddAspNetIdentity<User>()
            .AddTestUsers(Config.GetTestUsers());

        // Use AddAuthorizationBuilder to configure authorization policies
        builder.Services.AddAuthorizationBuilder()  
            .AddPolicy("RequirePolicePosition", policy =>
            {
                policy.RequireRole("Policeman");
                policy.RequireAuthenticatedUser();
            })
            .AddPolicy("RequirePoliceAdminPosition", policy =>
            {
                policy.RequireRole("Policeman", "OrganizationAdministrator");
                policy.RequireAuthenticatedUser();
            });
        // Add services to the container.
        builder.Services.AddAuthentication()
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Forbidden/";
                options.LoginPath = "/";
            });
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
        });

        builder.Services.AddAuthorization(options =>
        {
            // By default, all incoming requests will be authorized according to the default policy.
            options.FallbackPolicy = options.DefaultPolicy;
        });

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        app.MapIdentityApi<User>().AllowAnonymous();

        //app.UseAuthentication();
        app.UseIdentityServer();
        app.UseAuthorization();

        //using (var scope = app.Services.CreateScope())
        //{
        //    var services = scope.ServiceProvider;
        //    await RoleSeedExtension.SeedRolesAsync(services);
        //}

        app.MapGrpcService<Grpc.IdentityService>();

        app.MapControllers();

        app.Run();
    }
}
