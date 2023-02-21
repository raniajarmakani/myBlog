using Microsoft.Extensions.Configuration;
using MyBlog.Models.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using Dapper;

namespace MyBlog.Repository
{
    internal class UserRepository
    {
        private readonly IConfiguration _config;
        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IdentityResult> CreateAsync(UserIdentity user, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var dt = new DataTable();
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("NormelizesUserName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("NormelizedEmail", typeof(string));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Password", typeof(string));

            dt.Rows.Add(user.UserName,
                        user.NormalizedUserName,
                        user.Email,
                        user.NormalizedEmail,
                        user.FullName,
                        user.Password);
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync(ct);
                await connection.ExecuteAsync("user_insert", new { user = dt.AsTableValuedParameter("dbo.userType") }, commandType: CommandType.StoredProcedure);
            }
            return IdentityResult.Success;
        }
        public async Task<UserIdentity> GetByUserNameAsync(string normelizedUserName, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            UserIdentity user;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync(ct);
                user = await connection.QuerySingleOrDefaultAsync<UserIdentity>("user_getByUserName", new { NormalizedUserName = normelizedUserName }, commandType: CommandType.StoredProcedure);
            }
            return user;
        }

    }
}
