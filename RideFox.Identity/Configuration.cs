using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace RideFox.Identity;

public class Configuration
{
	private static readonly string _uri = "...";

	public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>()
	{
		new("RideFoxWebAPI", "Web API")
	};

	public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>()
	{
		new IdentityResources.OpenId(),
		new IdentityResources.Profile()
	};

	public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>()
	{
		new("RideFoxWebAPI", "Web API", new[] { JwtClaimTypes.Name })
		{
			Scopes = { "RideFoxWebAPI" }
		}
	};

	public static IEnumerable<Client> Clients => new List<Client>()
	{
		new()
		{
			ClientId = "ridefox-web-api",
			ClientName = "RideFox Web",
			AllowedGrantTypes = GrantTypes.Code,
			RequireClientSecret = false,
			RequirePkce = true,
			RedirectUris = // Набор uri адресов, куда будет перенаправлен клиент, после аунтификации
			{
				$"http://{_uri}/signin-oidc" // пока uri неизвестен, потом заменить ... на адрес клиентского приложения
			},
			AllowedCorsOrigins = // Набор uri адресов, которым доступен доступ к IdentityServer
			{
				$"http://{_uri}"
			},
			PostLogoutRedirectUris = // Набор uri адресов, куда будет перенаправлен клиент, после выхода
			{
				$"http://{_uri}/signout-oidc"
			},
			AllowedScopes = // Области доступные клиенту
			{
				IdentityServerConstants.StandardScopes.OpenId,
				IdentityServerConstants.StandardScopes.Profile,
				"RideFoxWebApi"
			},
			AllowAccessTokensViaBrowser = true, // Передача токена через браузер
			
		}
	};
}
