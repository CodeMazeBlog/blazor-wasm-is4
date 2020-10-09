using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWebAssembly.Client.Pages
{
	public partial class Index
	{
		[Inject]
		public IHttpClientFactory HttpClientFactory { get; set; }

		private string _message;

		protected override async Task OnInitializedAsync()
		{
			var client = HttpClientFactory.CreateClient("companyAPI.Unauthorized");
			var result = await client.GetFromJsonAsync<UnauthorizedTestDto>("companies/unauthorized");
			_message = result.Message;
		}
	}

	public class UnauthorizedTestDto
	{
		public string Message { get; set; }
	}
}
