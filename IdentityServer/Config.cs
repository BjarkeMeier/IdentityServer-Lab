using IdentityServer4.Models;
using IdentityServer4.Test;
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
        },

        // resource owner password grant client (deprecated)
        new Client
        {
            ClientId = "ro-center-client",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { new Secret("ro-center-client-secret".Sha256()) },
            AllowedScopes = { "world-api" }
        }
      };
    }

    public static List<TestUser> GetUsers()
    {
      return new List<TestUser>
      {
          new TestUser
          {
              SubjectId = "1",
              Username = "alice",
              Password = "password"
          },
          new TestUser
          {
              SubjectId = "2",
              Username = "bob",
              Password = "password"
          }
      };
    }
  }
}
