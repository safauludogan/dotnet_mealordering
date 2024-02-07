using Blazored.Modal;
using Blazored.Modal.Services;
using MealOrdering.Client.CustomComponents.ModalComponents;

namespace MealOrdering.Client.Utils
{
	public class ModalManager
	{
		private readonly IModalService modalService;
		public ModalManager(IModalService modalService)
        {
			this.modalService = modalService;

		}
        public async Task ShowMessageAsync(String Title, String message,int Duration =0)
		{
			ModalParameters mParams = new ModalParameters();
			mParams.Add("Message", message);
			var modelRef = modalService.Show<ShowMessagePopupComponent>(Title, mParams);

			if (Duration > 0)
			{
				await Task.Delay(Duration);
				modelRef.Close();
			}
			
			await modelRef.Result;
		}

		public async Task<bool> ConfirmationAsync(String Title, String message)
		{
			ModalParameters mParams = new ModalParameters();
			mParams.Add("Message", message);
			var modelRef = modalService.Show<ConfirmationPopupComponent>(Title, mParams);
			var modalResult = await modelRef.Result;
			return !modalResult.Cancelled;
		}
	}
}
