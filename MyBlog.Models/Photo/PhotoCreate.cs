using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Models.Photo
{
    public class PhotoCreate
    {
        public string ImgURL { get; set; }

        public string PublicId { get; set; }

        public string Description { get; set; }
    }
}
