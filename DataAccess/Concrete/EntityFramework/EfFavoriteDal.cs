using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFavoriteDal : EfEntityRepositoryBase<Favorite, NewsContext>, IFavoriteDal
    {
        public List<NewsDetailDto> GetFavoriteNewsDetails(int userId)
        {
            using (NewsContext context = new NewsContext())
            {
                var result = from f in context.Favorites
                             join n in context.News on f.NewsId equals n.Id
                             join c in context.Categories on n.CategoryId equals c.Id
                             join a in context.Authors on n.AuthorId equals a.Id
                             where f.UserId == userId
                             select new NewsDetailDto
                             {
                                 Id = n.Id,
                                 CategoryId = n.CategoryId,
                                 AuthorId = n.AuthorId,
                                 Title = n.Title,
                                 Content = n.Content,
                                 ImageUrl = n.ImageUrl,
                                 PublishDate = n.PublishDate,
                                 CategoryName = c.Name,
                                 AuthorName = a.FirstName + " " + a.LastName,
                                 Status = n.Status.ToString(),
                                 SpotText = n.SpotText,
                                 ViewCount = n.ViewCount
                             };
                return result.OrderByDescending(n => n.PublishDate).ToList();
            }
        }
    }
}
