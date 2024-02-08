using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.ResponseModels;
using System.Net.Http.Json;

namespace MealOrdering.Client.Utils
{
	public static class HttpClientExtension
	{

		public async static Task<TResult> PostGetServiceResponseAsync<TResult, TValue>(this HttpClient Client, String Url, TValue Value, bool ThrowSuccessException = false)
		{
			var httpRes = await Client.PostAsJsonAsync(Url, Value);

			if (httpRes.IsSuccessStatusCode)
			{
				var res = await httpRes.Content.ReadFromJsonAsync<ServiceResponse<TResult>>();

				return !res.IsSuccess && ThrowSuccessException ? throw new ApiException(res.Message) : res.Value;
			}

			throw new HttpException(httpRes.StatusCode.ToString());
		}

		public async static Task<BaseResponse> PostGetBaseResponseAsync<TValue>(this HttpClient Client, String Url, TValue Value, bool ThrowSuccessException = false)
		{
			var httpRes = await Client.PostAsJsonAsync(Url, Value);

			if (httpRes.IsSuccessStatusCode)
			{
				var res = await httpRes.Content.ReadFromJsonAsync<BaseResponse>();

				return !res.IsSuccess && ThrowSuccessException ? throw new ApiException(res.Message) : res;
			}

			throw new HttpException(httpRes.StatusCode.ToString());
		}

		public async static Task<T> GetServiceResponseAsync<T>(this HttpClient httpClient, string url, bool throwWhenNotSuccess = false)
		{
			var httpRes = await httpClient.GetFromJsonAsync<ServiceResponse<T>>(url);
			
			return !httpRes.IsSuccess && throwWhenNotSuccess ? throw new ApiException(httpRes.Message) : httpRes.Value;
		}
	}
}
