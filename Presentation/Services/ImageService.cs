﻿
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Presentation.Helpers;

namespace Presentation.Services
{
	public class ImageService : IImageService
	{
		private readonly Cloudinary _cloudinary;
        public ImageService(IOptions<CloudinarySettings> config)
        {
			var account = new Account(
				config.Value.CloudName, 
				config.Value.ApiKey, 
				config.Value.ApiSecret
				);
			_cloudinary = new Cloudinary( account );
        }
        public async Task<ImageUploadResult> UploadAsync(IFormFile file)
		{
			var result = await _cloudinary.UploadAsync(
				new ImageUploadParams()
				{
					File = new FileDescription(file.FileName, file.OpenReadStream()),
					DisplayName = file.FileName,
					Folder = "ArtworkUpload"
				}
				);
			if (result != null && result.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return result;
			}
			return null;
		}

		public async Task<DeletionResult> DeletePhotoAsync(string publicId)
		{
			var deleteParams = new DeletionParams(publicId);
			return await _cloudinary.DestroyAsync(deleteParams);
		}
	}
}
