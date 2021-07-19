using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.API.Configurations
{
    public static class IdentityConfiguration
    {
		public static IEnumerable<IdentityResource> IdentityResources =>
			new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResource("roles", new[] { "role" })
			};

		public static IEnumerable<ApiResource> Apis =>
			new ApiResource[]
			{
				new ApiResource()
                {
					Name = "catalogapi",
					Enabled = true,
					Scopes = new List<string> { "catalogapi" },
					UserClaims = new List<string> { "role" }
                }
			};

		public static IEnumerable<ApiScope> Scopes =>
			new ApiScope[]
			{
				new ApiScope("catalogapi")
			};

		public static IEnumerable<Client> Clients =>
			new List<Client>
			{
				new Client
                {
					ClientId = "client_id_catalog_swagger",
					ClientSecrets = {  new Secret("client_secret_catalog_swagger".ToSha256()) },
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					AllowedCorsOrigins = { "http://localhost:5000" },
					AllowedScopes =
                    {
						"roles",
						"catalogapi",
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile
                    }
                }
			};
	}
}
