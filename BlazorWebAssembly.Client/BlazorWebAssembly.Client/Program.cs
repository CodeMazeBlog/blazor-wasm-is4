using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using BlazorWebAssembly.Client.MessageHandler;
using BlazorWebAssembly.Client.ClaimsPrincipalFactory;
using SharedProject;

namespace BlazorWebAssembly.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

			builder.Services.AddHttpClient("companiesAPI", cl =>
			{
				cl.BaseAddress = new Uri("https://localhost:5001/api/");
			})
			.AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

			builder.Services.AddHttpClient("companyAPI.Unauthorized", client =>
				client.BaseAddress = new Uri("https://localhost:5001/api/"));

			builder.Services.AddScoped(
				sp => sp.GetService<IHttpClientFactory>().CreateClient("companiesAPI"));

			builder.Services.AddOidcAuthentication(options =>
			{
				builder.Configuration.Bind("oidc", options.ProviderOptions);
				options.UserOptions.RoleClaim = "role";
			})
			.AddAccountClaimsPrincipalFactory<MultipleRoleClaimsPrincipalFactory<RemoteUserAccount>>();

			builder.Services.AddAuthorizationCore(opt =>
			{
				opt.AddPolicy(
					Policy.CountryAndJobPosition,
					Policy.CountryAndJobPositionPolicy());
			});

			await builder.Build().RunAsync();
		}
	}
}
