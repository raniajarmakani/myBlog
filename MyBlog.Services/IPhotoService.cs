using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MyBlog.Services
{
    public interface IPhotoService
    {

        public Task<ImageUploadResult> AddPhotoAsync(IFormFile photo);
        public Task<DeletionResult> DeletePhotoAsync(string publicID);
      
    }
}
