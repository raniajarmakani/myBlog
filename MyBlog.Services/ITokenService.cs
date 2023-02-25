using MyBlog.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Services
{
    public interface ITokenService
    {
        public string  CreateToken(UserIdentity user);

    }
}
