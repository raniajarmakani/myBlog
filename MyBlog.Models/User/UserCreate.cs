using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Models.User
{
    public class UserCreate
    {
        [MinLength(10, ErrorMessage = "Minimum length is 10")]
        [MaxLength(30, ErrorMessage = "Maxumun length is 30")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "user name is required")]
        [MinLength(3, ErrorMessage = "Maxmum length is 5")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

    }
}
