using MyBlog.Models.Comment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public interface ICommentRepository
    {
        public Task<Comment> UpsertAsync(CommentCreate commentCreate, int userID);
        public Task<Comment> GetAsync(int commentID);
        public Task<List<Comment>> GetAllAsync(int blogID);
        public Task<int> DeleteAsync(int commentID);
    }
}
