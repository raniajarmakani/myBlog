using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Models.Comment
{
    public class CommentCreate
    {
        public int CommentID { get; set; }
        public int? ParentCommentID { get; set; }
        public int BlogId { get; set; }

        [Required(ErrorMessage = "content name is required")]
        [MinLength(10, ErrorMessage = "Minimum length is 10")]
        [MaxLength(300, ErrorMessage = "Maxumun length is 300")]
        public string Content { get; set; }

    }
}
