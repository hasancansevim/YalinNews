using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCommentDal : EfEntityRepositoryBase<Comment, NewsContext>, ICommentDal
    {
        public List<CommentDetailDto> GetCommentDetails(int newsId)
        {
            using (NewsContext context = new NewsContext())
            {
                var result = from c in context.Comments
                             join u in context.Users
                             on c.UserId equals u.Id
                             where c.NewsId == newsId
                             select new CommentDetailDto
                             {
                                 Id = c.Id,
                                 NewsId = c.NewsId,
                                 UserId = c.UserId,
                                 UserName = u.FirstName + " " + u.LastName,
                                 Content = c.Content,
                                 CreatedAt = c.CreatedAt
                             };
                return result.OrderByDescending(c => c.CreatedAt).ToList();
            }
        }
    }
}
