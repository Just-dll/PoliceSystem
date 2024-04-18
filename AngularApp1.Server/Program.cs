using AngularApp1.Server.Controllers;
using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using AngularApp1.Server.Services;
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

namespace AngularApp1.Server
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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
            builder.Services.AddDbContext<PolicedatabaseContext>();
            builder.Services.AddScoped<IMapper, Mapper>(services =>
            {
                var myProfile = new AutomapperProfile();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

                return new Mapper(configuration);
            });
            //builder.Services.AddAutoMapper(serv =>
            //{
            //    var myProfile = new AutomapperProfile();
            //    var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            //    serv.AddProfile(myProfile);
            //    serv.
            //});
            builder.Services.AddIdentityApiEndpoints<User>()
                .AddRoles<Position>()
                .AddEntityFrameworkStores<PolicedatabaseContext>();
            builder.Services.AddAuthorization((options) =>
            {
                options.AddPolicy("RequirePolicePosition", options =>
                {
                    options.RequireRole("Policeman");
                    options.RequireAuthenticatedUser();
                });
                options.AddPolicy("RequirePoliceAdminPosition", options =>
                {
                    options.RequireRole("Policeman");
                    options.RequireRole("OrganizationAdministrator");
                    options.RequireAuthenticatedUser();
                });
            });
            var smtpConfig = config.GetSection("Mail");
            var smtpCred = smtpConfig.GetSection("credential");
            var smtpConn = smtpConfig.GetSection("smtp");
            builder.Services.AddSingleton(new SmtpClient(smtpConn.GetSection("host").Value, int.Parse(smtpConn.GetSection("port").Value))
            {
                Credentials = new NetworkCredential(smtpCred.GetSection("userName").Value, smtpCred.GetSection("password").Value),
                EnableSsl = true
            });
            IConfigurationSection authSection =
                config.GetSection("Authentication");
            builder.Services.AddAuthentication()
            .AddCookie()
            .AddGoogle(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                var googleConfig = authSection.GetSection("Google");
                options.ClientId = googleConfig.GetSection("ClientId").Value;
                options.ClientSecret = googleConfig.GetSection("ClientSecret").Value;
                options.Scope.Add("openid");
            })
            .AddMicrosoftAccount(options =>
            {
                var microsoftConfig = authSection.GetSection("Microsoft");
                options.ClientId = microsoftConfig.GetSection("ClientId").Value;
                options.ClientSecret = microsoftConfig.GetSection("ClientSecret").Value;
            });
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddTransient<IEmailSender<User>, EmailSender>();
            builder.Services.AddScoped<IDrivingLicenseService, DrivingLicenseService>();
            
            var app = builder.Build();
            
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.MapIdentityApi<User>();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
