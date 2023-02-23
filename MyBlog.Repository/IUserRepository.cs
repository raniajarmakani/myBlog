using System.Threading.Tasks;
using System.Threading;
using MyBlog.Models.User;
using Microsoft.AspNetCore.Identity;

namespace MyBlog.Repository
{
    public interface IUserRepository
    {
        public Task<IdentityResult> CreateAsync(UserIdentity user, CancellationToken ct);
        public Task<UserIdentity> GetByUserNameAsync(string normelizedUserName, CancellationToken ct);

    }
}
