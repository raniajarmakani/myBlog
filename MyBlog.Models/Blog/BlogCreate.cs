using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Models.Blog
{
    public class BlogCreate
    {
        public int BlogId { get; set; }

        [Required(ErrorMessage = "title name is required")]
        [MinLength(10, ErrorMessage = "Minumum length is 10")]
        [MaxLength(50, ErrorMessage = "Maxmum length is 50")]
        public string Title { get; set; }

        [Required(ErrorMessage = "content is required")]
        [MinLength(300, ErrorMessage = "Minimum length is 300")]
        [MaxLength(3000, ErrorMessage = "Maximum length is 3000")]

        public string Content { get; set; }
        public int? PhotoID { get; set; }
    }
}
