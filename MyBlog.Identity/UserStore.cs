using Microsoft.AspNetCore.Identity;
using MyBlog.Models.User;
using MyBlog.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyBlog.Identity
{
    public class UserStore : IUserStore<UserIdentity>, IUserEmailStore<UserIdentity>, IUserPasswordStore<UserIdentity>
    {
        private readonly IUserRepository _userRepository;

        public UserStore(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> CreateAsync(UserIdentity user, CancellationToken cancellationToken)
        {
           return await _userRepository.CreateAsync(user, cancellationToken);
        }
        public async Task<UserIdentity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByUserNameAsync(normalizedUserName, cancellationToken);
        }

        
        public async Task<string> GetEmailAsync(UserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Email);
        }

        public async Task<bool> GetEmailConfirmedAsync(UserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);

        }

        public async Task<string> GetNormalizedEmailAsync(UserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.NormalizedEmail);
        }

        public async Task<string> GetNormalizedUserNameAsync(UserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.NormalizedUserName);
        }

        public async Task<string> GetPasswordHashAsync(UserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Password);
        }

        public async Task<string> GetUserIdAsync(UserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserID.ToString());
        }

        public async Task<string> GetUserNameAsync(UserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserName);
        }

        public async Task<bool> HasPasswordAsync(UserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Password!=null);
        }

        public Task SetEmailAsync(UserIdentity user, string email, CancellationToken cancellationToken)
        {
            user.Email= email;
            return Task.FromResult(0);

        }

        public Task SetEmailConfirmedAsync(UserIdentity user, bool confirmed, CancellationToken cancellationToken)
        {
           return Task.FromResult(confirmed);
        }

        public Task SetNormalizedEmailAsync(UserIdentity user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail= normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(UserIdentity user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName= normalizedName;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(UserIdentity user, string passwordHash, CancellationToken cancellationToken)
        {
           user.Password= passwordHash;
            return Task.FromResult(0);  
        }

        public Task SetUserNameAsync(UserIdentity user, string userName, CancellationToken cancellationToken)
        {
            user.UserName= userName;
            return Task.FromResult(0);
        }

        public Task<IdentityResult> UpdateAsync(UserIdentity user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(UserIdentity user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
          //nothung to dispose  throw new NotImplementedException();
        }

        public  Task<UserIdentity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserIdentity> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}
