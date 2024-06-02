using AngularApp1.Server.Controllers;
using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using AngularApp1.Server.Services;
using AngularApp1.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoMapper;
using BLL;
using Microsoft.AspNetCore.Authentication;
using BLL.Services;
using BLL.Interfaces;
using PoliceDAL.Interfaces;
using PoliceDAL.Repositories;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.BearerToken;
using RabbitMQ.Client;
using AngularApp1.Server.Extensions;

namespace AngularApp1.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // AddAsync services to the container.
            var config = builder.Configuration;
            builder.Services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var dbConnectionStrings = config.GetSection("ConnectionStrings");
            builder.Services.AddDbContext<PolicedatabaseContext>(options =>
            {
                options.UseSqlServer(dbConnectionStrings.GetSection("PoliceDB").Value);
            }, ServiceLifetime.Transient);

            builder.Services.AddScoped<IMapper, Mapper>(services =>
            {
                var myProfile = new AutomapperProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                return new Mapper(configuration);
            });

            builder.Services.AddIdentityApiEndpoints<User>()
                .AddRoles<Position>()
                .AddEntityFrameworkStores<PolicedatabaseContext>();

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

            var smtpConfig = config.GetSection("Mail");
            var smtpCred = smtpConfig.GetSection("credential");
            var smtpConn = smtpConfig.GetSection("smtp");
            builder.Services.AddSingleton(new SmtpClient(smtpConn["host"], int.Parse(smtpConn["port"]))
            {
                Credentials = new NetworkCredential(smtpCred["userName"], smtpCred["password"]),
                EnableSsl = true
            });

            IConfigurationSection authSection = config.GetSection("Authentication");
            builder.Services.AddAuthentication()
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Forbidden/";
                options.LoginPath = "/";
            })
            .AddGoogle(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                var googleConfig = authSection.GetSection("Google");
                options.ClientId = googleConfig["ClientId"];
                options.ClientSecret = googleConfig["ClientSecret"];
                options.Scope.Add("openid");
            })
            .AddMicrosoftAccount(options =>
            {
                var microsoftConfig = authSection.GetSection("Microsoft");
                options.ClientId = microsoftConfig["ClientId"];
                options.ClientSecret = microsoftConfig["ClientSecret"];
            });

            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddTransient<IEmailSender<User>, EmailSender>();
            builder.Services.AddScoped<IDrivingLicenseService, DrivingLicenseService>();
            builder.Services.AddSingleton<IConnection>(serviceProvider =>
            {
                var notifConfig = config.GetSection("NotificationServer");
                var factory = new ConnectionFactory()
                {
                    HostName = notifConfig["Hostname"],
                    Port = int.Parse(notifConfig["Port"]),
                    UserName = notifConfig["UserName"],
                    Password = notifConfig["Password"]
                };
                return factory.CreateConnection();
            });

            // Register NotificationService as transient
            builder.Services.AddTransient<NotificationService>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ICaseFileService, CaseFileService>();
            builder.Services.AddTransient<ProsecutorAssignationService>();
            builder.Services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(dbConnectionStrings.GetSection("Hangfire").Value);
            });
            builder.Services.AddHangfireServer();
            // Define a CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.WithOrigins("https://localhost:4200", "http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHangfireDashboard();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.MapIdentityApi<User>();

            // Apply the CORS policy
            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();
            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await RoleSeedExtension.SeedRolesAsync(services);
            }

            app.Run();
        }
    }
}
