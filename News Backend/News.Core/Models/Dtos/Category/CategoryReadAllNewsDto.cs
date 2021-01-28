using System;
using System.Collections.Generic;
using System.Text;
using News.Core.Models.Dtos.News;

namespace News.Core.Models.Dtos.Category
{
    public class CategoryReadAllNewsDto
    {
        public string Name { get; set; }

        public IEnumerable<NewsReadDto> News { get; set; }
    }
}
