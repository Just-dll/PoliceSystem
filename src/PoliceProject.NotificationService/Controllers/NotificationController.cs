using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoliceProject.NotificationService;
using PoliceProject.NotificationService.Grpc;
using System.Text.Json;

namespace AngularApp1.Server.Controllers
{
    public class NotificationController : ControllerBase
    {
        private readonly ServerSentNotificationEmitter emitter;
        public NotificationController(ServerSentNotificationEmitter emitter)
        {
            this.emitter = emitter;
        }

        [HttpGet("stream")]
        public async Task Stream(CancellationToken token)
        {
            var ctx = HttpContext;
            ctx.Response.Headers.Append("Content-Type", "text/event-stream");
            emitter.StartConsuming($"user_{HttpContext.User.GetPrincipalIdentifier()}");
            while (!token.IsCancellationRequested)
            {
                var item = await emitter.WaitForMessage();
                await ctx.Response.WriteAsync($"data: ");
                await JsonSerializer.SerializeAsync(ctx.Response.Body, item);
                await ctx.Response.WriteAsync($"\n\n");
                await ctx.Response.Body.FlushAsync();

                emitter.Reset();
            }
        }

    }
}
