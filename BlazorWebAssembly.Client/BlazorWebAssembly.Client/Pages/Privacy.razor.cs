using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWebAssembly.Client.Pages
{
	public partial class Privacy
	{ 
		[Inject]
		public HttpClient HttpClient { get; set; }

		private List<string> _claims = new List<string>();

		protected override async Task OnInitializedAsync()
		{
			_claims = await HttpClient.GetFromJsonAsync<List<string>>("companies/privacy");
		}
	}
}
