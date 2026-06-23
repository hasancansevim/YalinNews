using Core.Entities;
using System;

namespace Entities.DTOs
{
    public class CommentDetailDto : IDto
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
