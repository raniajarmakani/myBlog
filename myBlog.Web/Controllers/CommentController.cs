using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using MyBlog.Models.Comment;
using MyBlog.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myBlog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public  CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Comment>> Create(CommentCreate commentCreate)
        {
            int userID = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);
            Comment createdComment = await _commentRepository.UpsertAsync(commentCreate, userID);
            return Ok(createdComment);
        }

        [HttpGet("{blogId}")]
        public async Task<ActionResult<List<Comment>>> GetAll (int blogId)
        {
            var comments = await _commentRepository.GetAllAsync(blogId);
            return Ok(comments);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<int>> Delete (int commentId)
        {
            int userID = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);
            var commentFound = await _commentRepository.GetAsync(commentId);
            if (commentFound == null) { return BadRequest("The comment doesnt exist"); }
            if(commentFound.UserId == userID)
            {
                int affectedRows = await _commentRepository.DeleteAsync(commentId);
                return Ok(affectedRows);
            }
            else
            {
                return BadRequest("You didnt create this comment!");
            }

        }
    }
}
