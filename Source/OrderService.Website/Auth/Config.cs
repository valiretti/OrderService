using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace OrderService.Website.Auth
{
    public class Config
    {
        public const string ApiName = "api";

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource(ApiName, "API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {new Secret("secret".Sha256())},

                    AllowedScopes = new[]
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        ApiName
                    },
                    AllowOfflineAccess = true
                }
            };
        }

    }
}
