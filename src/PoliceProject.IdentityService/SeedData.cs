using IdentityModel;
using IdentityService.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityService;

public class SeedData
{
    private static readonly string[] Roles = { "Policeman", "OrganizationAdministrator", "Prosecutor", "Judge", "Civilian", "Attourney" };

    public async static Task EnsureSeedDataAsync(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Position>>();
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

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var alice = userMgr.FindByNameAsync("alice").Result;
            if (alice == null)
            {
                alice = new User
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Role, "Prosecutor")
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                await userMgr.AddToRolesAsync(alice, new[] { "Prosecutor", "Policeman", "OrganizationAdministrator" });
            }

            var bob = userMgr.FindByNameAsync("bob").Result;
            if (bob == null)
            {
                bob = new User
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere"),
                            new Claim(JwtClaimTypes.Role, "Judge")
                        }).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                await userMgr.AddToRolesAsync(bob, ["Prosecutor", "Policeman", "OrganizationAdministrator"]);
            }

            var dan = userMgr.FindByNameAsync("dan").Result;
            if (dan == null)
            {
                dan = new User
                {
                    UserName = "dan",
                    Email = "dan@fan.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(dan, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Dan Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Dan"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere"),
                            new Claim(JwtClaimTypes.Role, "Judge")
                        }).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

            }

            var alex = userMgr.FindByNameAsync("alex").Result;
            if (alex == null)
            {
                alex = new User
                {
                    UserName = "alex",
                    Email = "alex@fan.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(alex, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alex, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alex Petrov"),
                            new Claim(JwtClaimTypes.GivenName, "Alex"),
                            new Claim(JwtClaimTypes.FamilyName, "Petrov"),
                            new Claim(JwtClaimTypes.WebSite, "http://alex.com"),
                            new Claim("location", "somewhere"),
                            new Claim(JwtClaimTypes.Role, "Prosecutor")
                        }).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

            }
        }
    }
}