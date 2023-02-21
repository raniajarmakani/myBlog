using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Models.User
{
    public class UserIdentity
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
