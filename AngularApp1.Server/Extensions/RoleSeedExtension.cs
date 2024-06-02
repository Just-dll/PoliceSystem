using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace AngularApp1.Server.Extensions
{
    public static class RoleSeedExtension
    {
        private static readonly string[] Roles = { "Policeman", "OrganizationAdministrator", "Prosecutor", "Judge", "Civilian" };

        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Position>>();
            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Position()
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    });
                }
            }
        }
    }
}
