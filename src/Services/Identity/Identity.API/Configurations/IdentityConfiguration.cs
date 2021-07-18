using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace Identity.API.Configurations
{
    public static class IdentityConfiguration
    {
		public static List<TestUser> Users => new List<TestUser>
		
			{
				new TestUser
                {
					SubjectId = "fec0a4d6-5830-4eb8-8024-272bd5d6d2bb",
					Username = "User",
					Password = "password",
					Claims = new List<Claim>
                    {
						new Claim("role", "User")
                    }
                },
				new TestUser
                {
					SubjectId = "c3b7f625-c07f-4d7d-9be1-ddff8ff93b4d",
					Username = "Admin",
					Password = "password",
					Claims = new List<Claim>
                    {
						new Claim("role", "Admin")
                    }
                }
		};

		public static IEnumerable<ApiResource> Apis =>
			new ApiResource[]
			{
				new ApiResource()
                {
					Name = "festivalsapi",
					Enabled = true,
					Scopes = new List<string> { "festivalsapi" },
					UserClaims = new List<string> { "role" }
                }
			};

		public static IEnumerable<ApiScope> Scopes =>
			new ApiScope[]
			{
				new ApiScope("festivalsapi")
			};

		public static IEnumerable<Client> Clients =>
			new List<Client>
			{
				new Client
				{
					ClientName = "Postman",
					AllowOfflineAccess = true,
					AllowedScopes = new []
                    {
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"roles",
						"festivalsapi"
                    },
					RedirectUris = new []
                    {
						"https://www.getpostman.com/oauth2/callback"
                    },
					Enabled = true,
					ClientId = "832afa32-cabe-40a0-8909-2241cd85e47d",
					ClientSecrets = new []
                    {
						new Secret("NotASecret".ToSha256())
                    },
					PostLogoutRedirectUris = new []
                    {
						"http://localhost:5000/signout-callback-oidc"
                    },
					ClientUri = null,
					AllowedGrantTypes = new []
                    {
						GrantType.ResourceOwnerPassword
                    }
				}
			};
	}
}
