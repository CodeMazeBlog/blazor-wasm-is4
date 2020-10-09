using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorWebAssembly.Client.MessageHandler
{
	public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
	{
		public CustomAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation) 
			: base(provider, navigation)
		{
			ConfigureHandler(
				authorizedUrls: new[] { "https://localhost:5001" },
				scopes: new[] { "companyApi" });
		}
	}
}
