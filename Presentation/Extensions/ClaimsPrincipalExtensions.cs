using System.Security.Claims;

namespace Presentation.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string GetEmailAddress(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.Email)?.Value;
		}
		public static int GetUserId(this ClaimsPrincipal user)
		{
			return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
		}
	}
}
