using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.Models.Domain
{
    public class News
    {
        public int NewsId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DatePublished { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }

        public ICollection<UserEditedNews> UserEditedNews { get; set; }

        public News()
        {
            DatePublished = DateTime.Now;
            UserEditedNews = new HashSet<UserEditedNews>();
        }
    }
}
