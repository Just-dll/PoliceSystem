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
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.BearerToken;
using RabbitMQ.Client;
using BLL.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Principal;
using BLL.Grpc;
using DAL.Data;
using DAL.Interfaces;
using DAL.Repositories;
using QuestPDF.Infrastructure;

namespace MainService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //QuestPDF.Settings.License = LicenseType.Community;
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();
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
                options.UseSqlServer(dbConnectionStrings.GetSection("main").Value);
            }, ServiceLifetime.Transient);

            //builder.Services.AddIdentityApiEndpoints<User>()
            //    .AddRoles<Position>()
            //    .AddEntityFrameworkStores<PolicedatabaseContext>(); // to Identity
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

            var authSection = config.GetSection("Authentication");
            var identitySection = config.GetSection("Identity") ?? throw new ArgumentNullException("No identity section");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Forbidden/";
                options.LoginPath = "/";
            })
            .AddOpenIdConnect(options =>
            {
                options.Authority = identitySection["Url"];
                options.CallbackPath = "/signin-oidc";
                options.ClientId = "main";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.GetClaimsFromUserInfoEndpoint = true;
                options.RequireHttpsMetadata = false;
                options.Scope.Add("openid");
                options.Scope.Add("cases");
                options.Scope.Add("roles");
                options.SaveTokens = true;
                options.ClaimActions.MapJsonKey("role", "role", "role");
                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.RoleClaimType = "role";
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.WithOrigins("https://localhost:4200", "http://localhost:4200", identitySection["Url"])
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            builder.Services.AddGrpcClient<Identity.IdentityClient>(options =>
            {
                options.Address = new Uri(identitySection["Url"]);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                return handler;
            });
            var notificationSection = config.GetSection("Notification") ?? throw new ArgumentNullException("Notification section is not defined");
            builder.Services.AddGrpcClient<NotificationService.NotificationServiceClient>(options =>
            {
                options.Address = new Uri(notificationSection["Url"]);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                return handler;
            });

            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IDrivingLicenseService, DrivingLicenseService>();
            builder.Services.AddScoped<IUserService, IdentityService>();
            builder.Services.AddScoped<IMapper, Mapper>(services =>
            {
                var myProfile = new AutomapperProfile(services.GetRequiredService<Identity.IdentityClient>());
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                return new Mapper(configuration);
            });
            builder.Services.AddScoped<PdfGenerator>();
            //builder.Services.AddAutoMapper(cfg =>
            //{
            //    cfg.AddProfile<AutomapperProfile>();
            //});
            //builder.Services.AddTransient<IEmailSender<User>, EmailSender>(); // To identityService or notificationService
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ICaseFileService, CaseFileService>();
            builder.Services.AddTransient<IAssignationService, AssignationService>();
            builder.Services.AddTransient<IService<ReportModel>, ReportService>();
            
            //builder.Services.AddHangfire(x =>
            //{
            //    x.UseSqlServerStorage(dbConnectionStrings.GetSection("hangfire").Value);
            //});
            //builder.Services.AddHangfireServer();

            // Define a CORS policy

            var app = builder.Build();

            app.MapDefaultEndpoints();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseHangfireDashboard();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();

            // Apply the CORS policy
            app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<IdentityCheckerMiddleware>();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
