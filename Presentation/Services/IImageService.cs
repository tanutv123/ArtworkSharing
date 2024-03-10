using CloudinaryDotNet.Actions;

namespace Presentation.Services
{
	public interface IImageService
	{
		Task<ImageUploadResult> UploadAsync(IFormFile file);
		Task<DeletionResult> DeletePhotoAsync(string publicId);
		string GetImageUploadUrl(string url);
	}
}
