using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Models.User
{
    public class UserLogin
    {
        [Required(ErrorMessage = "user name is required")]
        [MinLength(5, ErrorMessage = "Minimum length is 5")]
        [MaxLength(20, ErrorMessage = "Maxumun length is 20")]
        public string Username { get; set; }

        [Required(ErrorMessage = "password is required")]
        [MinLength(10, ErrorMessage = "Minimum length is 10")]
        [MaxLength(50, ErrorMessage = "Maxumun length is 50")]
        public string Password { get; set; }
    }
}
