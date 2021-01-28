using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.Models.Dtos.News
{
    public class NewsReadDto
    {
        public int NewsId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DatePublished { get; set; }

        public DateTime DateEdited { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }
}
