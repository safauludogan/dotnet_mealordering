
namespace MealOrdering.Shared.CustomExceptions
{
	public class HttpException : Exception
	{
		public HttpException(String Message) : base(Message) { }

		public HttpException(String Message, Exception InnerException) : base(Message, InnerException) { }
	}
}
