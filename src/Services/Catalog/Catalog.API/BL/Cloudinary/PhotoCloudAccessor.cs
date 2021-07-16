﻿using Catalog.API.BL.Interfaces;
using Catalog.API.Startup.Settings;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Services
{
    public class PhotoCloudAccessor : IPhotoCloudAccessor
    {
        private readonly Cloudinary _cloudinary;

        private const int TransformedImageHeight = 500;

        private const int TransformedImageWidth = 500;

        private const string TransformationCropMode = "fill";

        private const string TransformationOverlayPosition = "face";

        public PhotoCloudAccessor(AppSettings appSettings, 
            IConfiguration configuration)
        {
            var account = new Account
            (
                appSettings.CloudinarySettings.CloudName,
                appSettings.CloudinarySettings.ApiKey,
                appSettings.CloudinarySettings.ApiSecret               
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<PhotoUploadResult> AddPhotoToCloudAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation()
                            .Height(TransformedImageHeight)
                            .Width(TransformedImageWidth)
                            .Crop(TransformationCropMode)
                            .Gravity(TransformationOverlayPosition)
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }
                

            return new PhotoUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.AbsoluteUri
            };
        }

        public async Task<string> DeletePhotoFromCloudAsync(string publicId)
        {
            const string CloudinarySuccessfulRemoveStatus = "ok";

            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == CloudinarySuccessfulRemoveStatus ? result.Result : default;
        }
    }
}
