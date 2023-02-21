using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Models.User
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
