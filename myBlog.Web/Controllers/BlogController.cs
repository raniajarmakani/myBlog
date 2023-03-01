using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using MyBlog.Models.Blog;
using MyBlog.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myBlog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IPhotoRepository _photoRepository;

        public BlogController(IBlogRepository blogRepository, IPhotoRepository photoRepository)
        {
            _blogRepository = blogRepository;
            _photoRepository = photoRepository;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Blog>> Create(BlogCreate blogCreate)
        {
            int userId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);
            if (blogCreate.PhotoID.HasValue)
            {
                var photo = await _photoRepository.GetAsync(blogCreate.PhotoID.Value);
                if(photo.UserID !=userId)
                {
                    return BadRequest("You didnt upload the photo");

                }

            }
            var blog = await _blogRepository.UpsertAsync(blogCreate, userId);
            return Ok(blog);
        }

        [HttpGet]
        public async Task<ActionResult<PageResult<Blog>>> GetAll([FromQuery]BlogPaging blogPaging)
        {
            var blogs = await _blogRepository.GetAllAsync(blogPaging);
            return Ok(blogs);
        }

        [HttpGet("{blogId}")]
        public async Task<ActionResult<Blog>> Get(int blogId)
        {
            var blog = await _blogRepository.GetAsync(blogId); 
            return Ok(blog);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Blog>>> GetAllByUserId(int userId)
        {
            var blogs = await _blogRepository.GetAllByUserIdAsync(userId);
            return Ok(blogs);
        }

        [HttpGet("famous")]
        public async Task<ActionResult<List<Blog>>> GetAllFamous()
        {
            var blogs = await _blogRepository.GetAllFamousAsync();
            return Ok(blogs);
        }

        [Authorize]
        [HttpDelete("{blogId}")]
        public async Task<ActionResult<int>> Delete(int blogId)
        {
            int userId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var blog = await _blogRepository.GetAsync(blogId);
            if(blog == null)
            
                return BadRequest("The blog doesn't exist");
            
            
                if(blog.UserId != userId)
                {
                    return BadRequest("You are not authorized to delete this blog");
                }
            else
            {
                int affectedRows = await _blogRepository.DeleteAsync(blogId);
                return Ok(affectedRows);
            }
           
           
        }

    }
}
