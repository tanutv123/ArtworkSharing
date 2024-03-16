namespace Presentation.Services
{
	public interface IValidator
	{
		Task<bool> ValidateEmail(string email);
	}
}
