using System.Text;

namespace MealOrdering.Shared.Utils
{
	public class PasswordEncrypter
	{
		public static string Encrypt(string password)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(password);
			return Convert.ToBase64String(plainTextBytes);
		}
	}
}
