using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.Models.Domain
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public ICollection<News> News { get; set; }

        public Category()
        {
            News = new HashSet<News>();
        }
    }
}
