using Identity.API.Startup.Settings;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
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
				},
				new ApiResource()
				{
					Name = "identityapi",
					Enabled = true,
					Scopes = new List<string> { "identityapi" },
					UserClaims = new List<string> { "role" }
				},
				new ApiResource()
                {
					Name = "basketapi",
					Enabled = true,
					Scopes = new List<string> { "basketapi" },
					UserClaims = new List<string> { "role" }
                }
			};

		public static IEnumerable<ApiScope> Scopes =>
			new ApiScope[]
			{
				new ApiScope("catalogapi"),
				new ApiScope("identityapi"),
				new ApiScope("basketapi")
			};

		public static IEnumerable<Client> GetClients(AppSettings appSettings) =>
			new List<Client>
			{
				new Client
				{
					ClientId = "client_id_catalog_swagger",
					ClientSecrets = {  new Secret("client_secret_catalog_swagger".ToSha256()) },
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					AllowedCorsOrigins = { appSettings.AppUrlsSettings.CatalogUrl },
					AllowedScopes =
					{
						"roles",
						"catalogapi",
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.OfflineAccess
					},
					AllowOfflineAccess = true
				},
				new Client
				{
					ClientId = "client_id_identity_swagger",
					ClientSecrets = {  new Secret("client_secret_identity_swagger".ToSha256()) },
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					AllowedCorsOrigins = { appSettings.AppUrlsSettings.IdentityUrl },
					AllowedScopes =
					{
						"roles",
						"identityapi",
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.OfflineAccess
					},
					AllowOfflineAccess = true
				},
				new Client
				{
					ClientId = "client_id_basket_swagger",
					ClientSecrets = {  new Secret("client_secret_basket_swagger".ToSha256()) },
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					AllowedCorsOrigins = { appSettings.AppUrlsSettings.BasketUrl },
					AllowedScopes =
					{
						"roles",
						"basketapi",
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.OfflineAccess
					},
					AllowOfflineAccess = true
				},
				new Client
				{
					ClientName = "Postman",
					AllowOfflineAccess = true,
					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"roles",
						"identityapi",
						"catalogapi",
						"basketapi"
					},
					RedirectUris =
					{
						"https://www.getpostman.com/oauth2/callback"
					},
					Enabled = true,
					ClientId = "postman_id",
					ClientSecrets =
					{
						new Secret("postman_secret".ToSha256())
					},
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword
				}
			};
	}
}
