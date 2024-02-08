using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MealOrdering.Client.Utils
{
	public class AuthStateProvider : AuthenticationStateProvider
	{
		private readonly ILocalStorageService _localStorageService;
		private readonly HttpClient _client;
		private readonly AuthenticationState _anonymous;

		public AuthStateProvider(ILocalStorageService localStorageService, HttpClient client)
        {
			_localStorageService = localStorageService;
			_client = client;
			_anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		}

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var apiToken = await _localStorageService.GetItemAsStringAsync("token");

			if (String.IsNullOrEmpty(apiToken))
				return _anonymous;

			var email = await _localStorageService.GetItemAsStringAsync("email");


			var cp = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) },"jwtAuthType"));

			_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);

			return new AuthenticationState(cp);
		}

		public void NotifyUserLogin(string email)
		{
			var cp = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }, "jwtAuthType"));
			var authState = Task.FromResult(new AuthenticationState(cp));

			NotifyAuthenticationStateChanged(authState);
		}
		public void NotifyUserLogout()
		{
			var authState = Task.FromResult(_anonymous);
			NotifyAuthenticationStateChanged(authState);

		}
	}
}
