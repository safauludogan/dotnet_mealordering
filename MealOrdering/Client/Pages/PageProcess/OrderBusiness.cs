﻿using Blazored.LocalStorage;
using MealOrdering.Client.Shared.FilterModels;
using MealOrdering.Client.Utils;
using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;

namespace MealOrdering.Client.Pages.PageProcess
{
	public class OrderBusiness : ComponentBase
	{
		[Inject]
		public HttpClient Client { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }
		[Inject]
		protected ILocalStorageService LocalStorage { get; set; }
		[Inject]
		protected ISyncLocalStorageService LocalStorageSync { get; set; }

		[Inject]
		ModalManager ModalManager { get; set; }

		public OrderListFilterModel filterModel = new OrderListFilterModel() { CreateDateFirst = DateTime.Now.Date, CreateDateLast = DateTime.Now.Date };

		protected List<OrderDto> OrderList;

		internal bool loading;

		protected override async Task OnInitializedAsync()
		{
			await ReLoadList();
		}

		protected string GetRemaningDateStr(DateTime ExpireDate)
		{
			TimeSpan ts = ExpireDate.Subtract(DateTime.Now);

			return ts.TotalSeconds >= 0 ? $"{ts.Hours}:{ts.Minutes}:{ts.Seconds}" : "00:00:00";
		}
		public void GoDetails(Guid SelectedOrderId)
		{
			NavigationManager.NavigateTo("/orders-items/" + SelectedOrderId.ToString());
		}


		public void GoCreateOrder()
		{
			NavigationManager.NavigateTo("/orders/add");
		}

		public void GoEditOrder(Guid OrderId)
		{
			NavigationManager.NavigateTo("/orders/edit/" + OrderId.ToString());
		}

		public async Task ReLoadList()
		{
			loading = true;

			try
			{
				OrderList = await Client.PostGetServiceResponseAsync<List<OrderDto>, OrderListFilterModel>("api/Order/OrdersByFilter", filterModel, true);
			}
			catch (ApiException ex)
			{
				await ModalManager.ShowMessageAsync("List Error", ex.Message);
			}
			finally
			{
				loading = false;
			}
		}
		public bool IsExpired(DateTime ExpireDate)
		{
			TimeSpan ts = ExpireDate.Subtract(DateTime.Now);
			return ts.TotalSeconds < 0;
		}

		public async Task DeleteOrder(Guid OrderId)
		{
			try
			{
				var modalRes = await ModalManager.ConfirmationAsync("Confirm", "Order will be deleted. Are you sure?");
				if (!modalRes)
					return;

				var res = await Client.GetServiceResponseAsync<BaseResponse>("api/Order/DeleteOrder/" + OrderId, true);

				OrderList.RemoveAll(i => i.Id == OrderId);
			}
			catch (ApiException ex)
			{
				await ModalManager.ShowMessageAsync("Deletion Error", ex.Message);
			}
		}

		public bool IsMyOrder(Guid CreatedUserId)
		{
			return true;
			//return LocalStorageSync.GetUserIdSync() == CreatedUserId;
		}
	}
}
