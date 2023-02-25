using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MyBlog.Models.Settings;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Services
{
    public class PhotoService : IPhotoServices
    {
        private Cloudinary _cloudinary;
        public  PhotoService(IOptions<CloudinaryOptions> config)
        {
            var account = new Account(config.Value.cloudName, config.Value.ApiKey,config.Value.ApiSecret);
            _cloudinary= new Cloudinary(account);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile photo)
        {
            var uploadResult= new ImageUploadResult();

            if(photo.Length> 0)
            {
                using(var stream = photo.OpenReadStream())
                {
                    var uploadParam = new ImageUploadParams
                    {
                        File = new FileDescription(photo.FileName, stream),
                        Transformation = new Transformation().Width(300).Height(500).Crop("fill")

                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParam);

                }

            }

            return uploadResult;


        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicID)
        {
            var deletionParams = new DeletionParams(publicID);
            var result = await _cloudinary.DestroyAsync(deletionParams);
            return result;

                
        }
    }
}
