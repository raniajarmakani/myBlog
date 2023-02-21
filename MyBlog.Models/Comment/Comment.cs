using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Models.Comment
{
    public class Comment : CommentCreate
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime publishDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}