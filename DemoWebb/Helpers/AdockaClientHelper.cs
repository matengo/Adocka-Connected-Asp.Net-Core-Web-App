using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoWebb.Helpers
{
    public class AdockaClientHelper : IAdockaClientHelper
    {
        private IMemoryCache _cache;
        private HttpClient _client;
        private IConfiguration _config;
        public AdockaClientHelper(IMemoryCache cache, IConfiguration config)
        {
            _config = config;
            _cache = cache;

            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://api.adocka.com");
            
        }
        public async Task<HttpClient> GetClient()
        {
            var bearer = await GetBearer();
            _client.SetBearerToken(bearer);
            return _client;
        }

        private async Task<string> GetBearer()
        {
            var bearer = await _cache.GetOrCreateAsync<string>("bearer", async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = TimeSpan.FromHours(6);

                var disco = await DiscoveryClient.GetAsync("https://identity.adocka.com");
                var tokenClient = new TokenClient(disco.TokenEndpoint, _config.GetValue<string>("ClientId"), _config.GetValue<string>("ClientSecret"));
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
                return tokenResponse.AccessToken;
            });
            return bearer;
            
        }
    }
}
