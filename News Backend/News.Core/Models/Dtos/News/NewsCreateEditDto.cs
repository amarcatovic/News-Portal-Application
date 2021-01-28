using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.Models.Dtos.News
{
    public class NewsCreateEditDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; }
    }
}
