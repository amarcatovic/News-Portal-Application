using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.Core.Models.Dtos.News
{
    public class NewsCreateEditDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
