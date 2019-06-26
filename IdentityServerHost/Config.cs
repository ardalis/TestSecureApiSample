using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServerHost
{
    public class Config
    {
        public const string BASE_URL = "http://localhost:3611";

        public static IEnumerable<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "spa",
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RequireClientSecret = false,
                RequirePkce = true,
                RequireConsent = false,
                RedirectUris = {
                    $"{BASE_URL}/callback.html",
                    $"{BASE_URL}/popup.html",
                    $"{BASE_URL}/silent.html"
                },
                PostLogoutRedirectUris = { $"{BASE_URL}/index.html" },
                AllowedScopes = { "openid", "profile", "email", IdentityServerConstants.LocalApi.ScopeName },
                AllowedCorsOrigins = { BASE_URL }
            },
        };

        public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

        public static IEnumerable<ApiResource> Apis = new List<ApiResource>
        {
            // local API
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
        };
    }
}
