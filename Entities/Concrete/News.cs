using Core.Entities;
using Entities.Enums;
using System;

namespace Entities.Concrete
{
    public class News : IEntity
    {
        public int Id { get; set; }
        public string? Slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public NewsStatus Status { get; set; } = NewsStatus.Draft;
        public string? SpotText { get; set; }
        public int ViewCount { get; set; } = 0;
    }
}