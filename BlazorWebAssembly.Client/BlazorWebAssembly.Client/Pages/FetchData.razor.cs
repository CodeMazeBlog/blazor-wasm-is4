using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWebAssembly.Client.Pages
{
	public partial class FetchData
	{
		[Inject]
		public HttpClient Http { get; set; }
		private CompanyDto[] _companies;

		protected override async Task OnInitializedAsync()
		{
			_companies = await Http.GetFromJsonAsync<CompanyDto[]>("companies");
		}
	}

	public class CompanyDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string FullAddress { get; set; }
	}
}
