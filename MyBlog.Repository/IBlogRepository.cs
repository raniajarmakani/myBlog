using MyBlog.Models.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public interface IBlogRepository
    {
        public Task<Blog> UpsertAsync(BlogCreate blogCreate, int userID);


        public Task<PageResult<Blog>> GetAllAsync(BlogPaging blogs);

        public Task<Blog> GetAsync(int blogID);

        public Task<List<Blog>> GetAllByUserIdAsync(int userID);

        public Task<List<Blog>> GetAllFamousAsync();

        public Task<int> DeleteAsync(int blogId);



    }
}
