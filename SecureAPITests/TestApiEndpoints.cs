using IdentityModel.Client;
using IdentityServer4;
using IdentityServerHost;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SecureAPITests
{
    public class TestApiEndpoints
    {
        public string IdentityBaseUrl { get; set; } = Config.BASE_URL;
        public string ApiBaseUrl { get; set; } = Config.BASE_URL;

        public TestApiEndpoints()
        {
            string identityBaseUrl = Environment.GetEnvironmentVariable("IdentityBaseUrl");
            if (!String.IsNullOrEmpty(identityBaseUrl))
            {
                IdentityBaseUrl = identityBaseUrl;
            }
            string apiBaseUrl = Environment.GetEnvironmentVariable("ApiBaseUrl");
            if (!String.IsNullOrEmpty(apiBaseUrl))
            {
                ApiBaseUrl = apiBaseUrl;
            }
        }

        private async Task<string> GetAccessToken()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(IdentityBaseUrl);
            if (!String.IsNullOrEmpty(disco.Error))
            {
                throw new Exception(disco.Error);
            }
            var response = await client.RequestTokenAsync(new TokenRequest
            {
                Address = disco.TokenEndpoint,
                GrantType = IdentityModel.OidcConstants.GrantTypes.ClientCredentials,
                ClientId = "spa",

                Parameters =
                {
                    { "username", "alice"},
                    { "password", "alice"},
                    { "scope", IdentityServerConstants.LocalApi.ScopeName }
                }
            });
            return response.AccessToken;
        }

        [Fact]
        public async Task GetAccessTokenWithAliceCreds()
        {
            string token = await GetAccessToken();

            Assert.False(string.IsNullOrWhiteSpace(token));
        }

        [Fact]
        public async Task HitApiEndpoint()
        {
            string token = await GetAccessToken();

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(token);

            var apiResponse = await apiClient.GetAsync($"{ApiBaseUrl}/test");

            Assert.True(apiResponse.IsSuccessStatusCode);

            var stringResponse = await apiResponse.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeAnonymousType(stringResponse, new { message = "" });
            Assert.Equal("Hello API", result.message);
        }

        [Fact]
        public async Task GetPublicHealthEndpoint()
        {
            var apiClient = new HttpClient();

            var apiResponse = await apiClient.GetAsync($"{ApiBaseUrl}/health");

            Assert.True(apiResponse.IsSuccessStatusCode);

            var stringResponse = await apiResponse.Content.ReadAsStringAsync();

            Assert.Equal("Healthy", stringResponse);
        }
    }
}
