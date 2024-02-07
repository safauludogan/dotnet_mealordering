namespace MealOrdering.Server.Services.Infasracture
{
	public interface IValidationService
	{
		public bool IsAdmin(Guid UserId);

		public bool HasPermissionToChange(Guid UserId);

		public bool HasPermission(Guid UserId);
	}
}
