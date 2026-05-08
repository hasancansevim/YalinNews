using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class NewsDetailDto:IDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public string Status { get; set; }
        public string? SpotText { get; set; }
        public int ViewCount { get; set; }
    }
}