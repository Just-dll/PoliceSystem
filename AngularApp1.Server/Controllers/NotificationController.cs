using AngularApp1.Server.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AngularApp1.Server.Controllers
{
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService service;
        private readonly UserManager<User> userManager;
        public NotificationController(NotificationService service, UserManager<User> manager)
        {
            this.service = service;
            userManager = manager;
        }

        [HttpGet("stream")]
        public async Task Stream(CancellationToken token)
        {
            var ctx = HttpContext;
            var user = await userManager.GetUserAsync(ctx.User);
            if(user == null)
            {
                return;
            }
            ctx.Response.Headers.Append("Content-Type", "text/event-stream");
            service.StartConsuming($"user_{user.Id}");
            while (!token.IsCancellationRequested)
            {
                var item = await service.WaitForMessage();
                await ctx.Response.WriteAsync($"data: ");
                await JsonSerializer.SerializeAsync(ctx.Response.Body, item);
                await ctx.Response.WriteAsync($"\n\n");
                await ctx.Response.Body.FlushAsync();

                service.Reset();
            }
        }

    }
}
