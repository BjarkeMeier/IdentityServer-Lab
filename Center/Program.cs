using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Center
{
  class Program
  {
    static void Main(string[] args)
    {
      new Program().MainAsync().GetAwaiter().GetResult();
    }

    private async Task MainAsync()
    {
      // Get access token for World API from IdentityServer
      var accessToken = await GetAccessTokenAsync();

      // Call protected World API (/api/identity) using obtained access token
      var client = new HttpClient();
      client.SetBearerToken(accessToken);

      var response = await client.GetAsync("http://localhost:5001/api/identity");
      if (!response.IsSuccessStatusCode)
        throw new Exception(response.StatusCode.ToString());

      var content = await response.Content.ReadAsStringAsync();
      Console.WriteLine(JArray.Parse(content));
    }

    private async Task<string> GetAccessTokenAsync()
    {
      // Discover endpoints from metadata
      var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

      // Request access token for the World API
      var tokenClient = new TokenClient(disco.TokenEndpoint, "center-client", "center-client-secret");
      var tokenResponse = await tokenClient.RequestClientCredentialsAsync("world-api");

      if (tokenResponse.IsError)
        throw new Exception(tokenResponse.Error);

      Console.WriteLine(tokenResponse.Json);

      return tokenResponse.AccessToken;
    }
  }
}
