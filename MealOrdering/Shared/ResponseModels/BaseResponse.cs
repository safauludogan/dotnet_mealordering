namespace MealOrdering.Shared.ResponseModels
{
	public class BaseResponse
	{
        public BaseResponse()
        {
            IsSuccess = true;
        }
        public bool IsSuccess{ get; set; }
        public  string Message { get; set; }
        public void SetException(Exception ex)
        {
            IsSuccess = false;
            Message = ex.Message;
        }
    }
}
