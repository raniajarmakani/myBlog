using Microsoft.Extensions.Configuration;
using MyBlog.Models.Blog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MyBlog.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IConfiguration _config;
        public BlogRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> DeleteAsync(int blogId)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                affectedRows = await connection.ExecuteAsync("blog_delete",
                                                           new { blogId = blogId },
                                                           commandType: CommandType.StoredProcedure);
            }

            return affectedRows;
        }


        public async Task<PageResult<Blog>> GetAllAsync(BlogPaging blogPaging)
        {
            var results = new PageResult<Blog>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                using (var multi = connection.QueryMultipleAsync("blog_getAll",
                                                                 new
                                                                 {
                                                                     offset = (blogPaging.Page - 1) * blogPaging.PageSize,
                                                                     pageSize = blogPaging.PageSize

                                                                 },
                                                                 commandType: CommandType.StoredProcedure))
                {

                    results.Items = (IEnumerable<Blog>)multi.Result.Read();
                    results.TotalCount = multi.Result.ReadFirst();

                }

            }
            return results;
        }


        public async Task<List<Blog>> GetAllByUserIdAsync(int userID)
        {
            IEnumerable<Blog> result;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                result = await connection.QueryAsync<Blog>("blog_GetByUserID",
                                                     new
                                                     {
                                                         userID = userID
                                                     },
                                                     commandType: CommandType.StoredProcedure);
            }

            return result.ToList();
        }

        public async Task<List<Blog>> GetAllFamousAsync()
        {
            IEnumerable<Blog> result;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                result = await connection.QueryAsync<Blog>("blog_getTop6",
                                                     new
                                                     {

                                                     },
                                                     commandType: CommandType.StoredProcedure);
            }

            return result.ToList();
        }

        public async Task<Blog> GetAsync(int blogID)
        {
            Blog blog;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                blog = await connection.QueryFirstOrDefaultAsync<Blog>("blog_get",
                                                     new
                                                     {
                                                         blogID = blogID
                                                     },
                                                     commandType: CommandType.StoredProcedure);
            }

            return blog;
        }

        public async Task<Blog> UpsertAsync(BlogCreate blogCreate, int userID)
        {
            int? newBlogID;
            DataTable dt = new DataTable();
            dt.Columns.Add("BlogId", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Content", typeof(string));
            dt.Columns.Add("PhotoID", typeof(int));

            dt.Rows.Add(blogCreate.BlogId, blogCreate.Title, blogCreate.Content, blogCreate.PhotoID);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                newBlogID = await connection.ExecuteScalarAsync<int?>("blog_upsert",
                                                     new
                                                     {
                                                         Blog = dt.AsTableValuedParameter("dbo.BlogType"),
                                                         userID = userID
                                                     },
                                                     commandType: CommandType.StoredProcedure);
            }

            newBlogID = newBlogID ?? blogCreate.BlogId;
            Blog returnedBlog = await GetAsync(newBlogID.Value);
            return returnedBlog;
        }

        
    }

}
