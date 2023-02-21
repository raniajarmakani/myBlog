using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Models.Blog
{
    public class Blog
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
