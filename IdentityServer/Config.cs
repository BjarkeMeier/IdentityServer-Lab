using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
  public abstract class Config
  {
    public static List<ApiResource> GetApiResources()
    {
      return new List<ApiResource>
      {
        new ApiResource(name: "world-api", displayName: "World API")
      };
    }

    public static List<Client> GetClients()
    {
      return new List<Client>
      {
        new Client
        {
          ClientId = "center-client",

          // no interactive user, use the clientid/secret for authentication
          AllowedGrantTypes = GrantTypes.ClientCredentials,

          ClientSecrets = { new Secret("center-client-secret".Sha256()) },

          AllowedScopes = {"world-api" }
        }
      };
    }
  }
}
