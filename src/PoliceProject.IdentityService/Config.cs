using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using System.Text.Json;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<ApiScope> GetScopes()
    {
        return
        [
            new ApiScope("cases", "Read and write to case files"),
            new ApiScope("tickets", "Ticket management"),
            new ApiScope("notifications", "Notification receive"),
        ];
    }

    public static IEnumerable<IdentityResource> GetResources()
    {
        return
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource {
                Name = "roles",
                UserClaims = { ClaimTypes.Role },
            }
        ];
    }

    public static IEnumerable<ApiResource> GetApiResources()
    {
        return
        [
            new ApiResource("main", "Main service")
            {
                Scopes = { "cases", "tickets" },
                UserClaims = { ClaimTypes.Role },
            },
            new ApiResource("notification", "Notification service"),
        ];
    }

    public static IEnumerable<Client> GetClients(IConfiguration configuration)
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "main",
                ClientName = "Main api Client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "roles",
                    "cases",
                    "tickets",
                },
                AllowedGrantTypes = GrantTypes.Code,
                AccessTokenLifetime = 60 * 60 * 2, // 2 hours
                IdentityTokenLifetime = 60 * 60 * 2, // 2 hours
                RedirectUris = { configuration["MainApiClient"] + "/signin-oidc" },
            },
            new Client
            {
                ClientId = "web",
                ClientName = "Web client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    IdentityServerConstants.StandardScopes.Email,
                    "cases",
                    "tickets"
                },
            },
            new Client
            {
                ClientId = "notification",
                ClientName = "Notification service",
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    "notifications"
                },
                RedirectUris = { configuration["NotificationServiceClient"] },
            }
        };
    }

    internal static List<TestUser> GetTestUsers()
    {
        var address = new
        {
            street_address = "One Hacker Way",
            locality = "Heidelberg",
            postal_code = "69118",
            country = "Germany"
        };

        return new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "alice",
                Password = "alice",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                }
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "bob",
                Password = "bob",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Bob"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                }
            }
        };
    }
}
