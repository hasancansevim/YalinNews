using Core.Entities;
using System;

namespace Entities.DTOs
{
    public class SitemapNewsDto : IDto
    {
        public string Slug { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
