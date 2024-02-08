using MealOrdering.Client.Utils;
using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace MealOrdering.Client.Pages.PageProcess
{
	public class UserListProccess : ComponentBase
	{
		[Inject]
		public HttpClient Client { get; set; }
		protected List<UserDto> userList = new List<UserDto>();

		[Inject]
		ModalManager ModalManager { get; set; }

		[Inject]
		NavigationManager NavigationManager { get; set; }

		protected async override void OnInitialized()
		{
			//base.OnInitialized();
			await LoadList();
		}

		protected async Task LoadList()
		{
			try
			{
				userList = await Client.GetServiceResponseAsync<List<UserDto>>("api/User/GetUsers", true);
			}
			catch (ApiException ex)
			{
				await ModalManager.ShowMessageAsync("Api Exception", ex.Message);
			}
			catch (Exception ex)
			{
				await ModalManager.ShowMessageAsync("Api Exception", ex.Message);
			}

			StateHasChanged();
		}

		protected void goCreateUserPage()
		{
			NavigationManager.NavigateTo("/users/add");
		}
		protected void goUpdateUserPage(Guid userId)
		{
			NavigationManager.NavigateTo($"/users/edit/{userId}");
		}
		protected async Task deleteUser(Guid userId)
		{
			bool confirmed = await ModalManager.ConfirmationAsync("Confirmation", "User will be deleted. Are you sure?");
			if (!confirmed) return;
			try
			{//DeleteUser
				bool deleted = await Client.PostGetServiceResponseAsync<bool, Guid>("api/User/DeleteUser", userId, true);
				await LoadList();
			}
			catch (ApiException ex)
			{
				await ModalManager.ShowMessageAsync("User Deletion Error", ex.Message);
			}
			catch (Exception ex)
			{
				await ModalManager.ShowMessageAsync("An Error", ex.Message);
			}
		}
	}
}
