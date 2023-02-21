using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Models.Photo
{
    public class Photo : PhotoCreate
    {
        public int PhotoId { get; set; }
        public int UserID { get; set; }
        public DateTime publishDate { get; set; }
        public DateTime updateDate { get; set; }


    }
}
