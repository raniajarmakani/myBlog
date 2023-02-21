using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Models.Blog
{
   
        public class PageResult<T>
        {
            public IEnumerable<T> Items { get; set; }

            public int TotalCount { get; set; }

        }
    
}
