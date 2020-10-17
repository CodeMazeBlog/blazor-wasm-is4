using Microsoft.AspNetCore.Authorization;

namespace SharedProject
{
	public static class Policy 
	{ 
		public const string CountryAndJobPosition = "CountryAndJobPosition"; 
		public static AuthorizationPolicy CountryAndJobPositionPolicy() 
			=> new AuthorizationPolicyBuilder()
			.RequireAuthenticatedUser()
			.RequireClaim("country", "USA")
			.RequireClaim("position", "Administrator")
			.Build(); 
	}
}
