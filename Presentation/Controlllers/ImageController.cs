using Microsoft.AspNetCore.Mvc;
using Presentation.Services;
using System.Net;

namespace Presentation.Controlllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ImageController : Controller
	{
		private readonly IImageService _imageService;

		public ImageController(IImageService imageService)
        {
			_imageService = imageService;
		}
		[HttpPost]
		public async Task<IActionResult> UploadAsync(IFormFile file)
		{
			var result = await _imageService.UploadAsync(file);
			if (result == null)
			{
				ModelState.AddModelError("Upload image", " Something went wrong");
				return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
			}
			return Json(new { link = result.SecureUri.AbsoluteUri, publicId = result.PublicId });

		}
	}
}
