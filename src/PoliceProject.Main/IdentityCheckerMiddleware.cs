using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MainService
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class IdentityCheckerMiddleware
    {
        private readonly RequestDelegate _next;

        public IdentityCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, PolicedatabaseContext context)
        {
            try
            {
                var principalIdentifier = httpContext.User.GetPrincipalIdentifier() ?? throw new UnauthorizedAccessException();

                var user = context.Users.FirstOrDefault(u => u.IdentityId == principalIdentifier);

                if (user == null)
                {
                    user = new User
                    {
                        IdentityId = principalIdentifier,
                    };

                    context.Users.Add(user);
                    await context.SaveChangesAsync();

                }

                httpContext.Items.Add(nameof(User), user);
            }
            catch (Exception)
            {

            }
            finally
            {
                await _next(httpContext);
            }
        }
    }

    public static class IdentityMiddlewareExtensions
    {
        public static IApplicationBuilder UseIdentityMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdentityCheckerMiddleware>();
        }
    }
}
