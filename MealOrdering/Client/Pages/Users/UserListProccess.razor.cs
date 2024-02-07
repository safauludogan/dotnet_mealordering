using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace MealOrdering.Client.Pages.Users
{
	public class UserListProccess : ComponentBase
	{
		[Inject]
		public HttpClient Client { get; set; }
		protected List<UserDto> userList = new List<UserDto>();




		protected async override void OnInitialized()
		{
			//base.OnInitialized();
			await LoadList();
		}

		protected async Task LoadList()
		{
			var serviceResponse = await Client.GetFromJsonAsync<ServiceResponse<List<UserDto>>>("api/User/GetUsers");

			if (serviceResponse.IsSuccess)
				userList = serviceResponse.Value;

			StateHasChanged();
		}
	}
}
