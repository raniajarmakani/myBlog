using Microsoft.Extensions.Configuration;
using MyBlog.Models.Comment;
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
    public class CommentRepository : ICommentRepository
    {
        private readonly IConfiguration _config;
        public CommentRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> DeleteAsync(int commentID)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                affectedRows = await connection.ExecuteAsync("comment_delete",
                                                           new { commentID = commentID },
                                                           commandType: CommandType.StoredProcedure);
            }

            return affectedRows;

        }

        public async Task<List<Comment>> GetAllAsync(int blogID)
        {
            IEnumerable<Comment> comments = null;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                comments = await connection.QueryAsync<Comment>("comment_getAll",
                                                           new { blogID = blogID },
                                                           commandType: CommandType.StoredProcedure);
            }

            return comments.ToList();
        }

        public async Task<Comment> GetAsync(int commentID)
        {
            Comment comment;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                comment = await connection.QueryFirstOrDefault("comment_get",
                                                           new { commentID = commentID },
                                                           commandType: CommandType.StoredProcedure);
            }

            return comment;
        }

        public async Task<Comment> UpsertAsync(CommentCreate commentCreate, int userID)
        {
            int? newCommentID;
            DataTable dt = new DataTable();
            dt.Columns.Add("CommentID", typeof(int));
            dt.Columns.Add("ParentCommentID", typeof(int));
            dt.Columns.Add("BlogID", typeof(int));
            dt.Columns.Add("Content", typeof(string));

            dt.Rows.Add(commentCreate);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                newCommentID = await connection.ExecuteScalarAsync<int?>("comment_upsert",
                                                           new
                                                           {
                                                               comment = dt.AsTableValuedParameter("dbo.commentType"),
                                                               userID = userID
                                                           },
                                                           commandType: CommandType.StoredProcedure);
            }
            newCommentID = newCommentID ?? commentCreate.CommentID;
            Comment comment = await GetAsync(newCommentID.Value);
            return comment;
        }

        Task<List<Comment>> ICommentRepository.GetAllAsync(int blogID)
        {
            throw new NotImplementedException();
        }
    }
}

