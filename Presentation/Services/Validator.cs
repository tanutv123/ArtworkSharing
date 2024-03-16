using System.Net.Http.Headers;

namespace Presentation.Services
{
	public class Validator : IValidator
	{
		public async Task<bool> ValidateEmail(string email)
		{
			using HttpClient client = new();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Add("User-Agent", "ASP");
			var json = await client.GetStringAsync(
				"https://emailvalidation.abstractapi.com/v1/?api_key=887d421a032a4a13b47dec623ae6ccee&email=" + email);

			var result = json.Contains("UNDELIVERABLE");
			return result;
		}
	}
}
