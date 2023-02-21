using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNet.Identity;
using MyBlog.Models.User;

namespace MyBlog.Repository
{
    internal interface IUserRepository
    {
        public Task<IdentityResult> CreateAsync(UserIdentity user, CancellationToken ct);
        public Task<UserIdentity> GetByUserNameAsync(string normelizedUserName, CancellationToken ct);

    }
}
