using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using MyBlog.Models.Photo;
using MyBlog.Repository;
using MyBlog.Services;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace myBlog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoRepository photoRepository, IBlogRepository blogRepository, IPhotoService photoService)
        {
            _photoRepository = photoRepository;
            _blogRepository = blogRepository;
            _photoService = photoService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Photo>> UploadPhoto (IFormFile photoFile)
        {
            int userId = int.Parse(User.Claims.First(i =>i.Type== System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.NameId).Value);
            var uploadResult = await _photoService.AddPhotoAsync(photoFile);
            if(uploadResult.Error!= null) return BadRequest(uploadResult.Error.Message);
            var photoCreate = new PhotoCreate
            {
                PublicId = uploadResult.PublicId,
                ImgURL = uploadResult.SecureUrl.AbsoluteUri,
                Description = photoFile.FileName
            };
            var photo = await _photoRepository.InsertAsync(photoCreate,userId);
            return Ok(photo);
        }

        [Authorize]
        [HttpGet]

        public async Task<ActionResult<List<Photo>>> GetAllPhotosByUserId()
        {
            int userId = int.Parse(User.Claims.First(i => i.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.NameId).Value);
            var photos = await _photoRepository.GetAllByUserId(userId);
            return Ok(photos);
        }

        [HttpGet("{photoId}")]
        public async Task<ActionResult<Photo>> Get(int photoId)
        {
            var photo = await _photoRepository.GetAsync(photoId);
            return Ok(photo);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<int>> Delete (int photoId)
        {
            int userId = int.Parse(User.Claims.First(i => i.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.NameId).Value);
            var photoFound = await _photoRepository.GetAsync(photoId);
            if (photoFound != null)
            {
                if(userId == photoFound.UserID)
                {
                    var blogs= await _blogRepository.GetAllByUserIdAsync(userId);
                    var blogsHasPhoto = blogs.Any(b=> b.PhotoID == photoId);
                    if (blogsHasPhoto) { return BadRequest("This photo is used by other blogs, cant be deleted"); }
                    var i = await _photoService.DeletePhotoAsync(photoFound.PublicId);
                    if (i != null) { return BadRequest(i.Error.Message); }
                    var affactedRows = await _photoRepository.DeleteAsync(photoFound.PhotoID);
                    return Ok(affactedRows);

                }
                else {
                    return BadRequest("You are trying to delete a photo that is not yours");
                }

            }
            else { 
                return BadRequest("Photo not found"); 
            }
        }
    }
}
