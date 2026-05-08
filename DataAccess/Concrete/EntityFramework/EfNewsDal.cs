using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfNewsDal : EfEntityRepositoryBase<News, NewsContext>, INewsDal
    {

        public List<NewsDetailDto> GetNewsDetail(int page = 1, int pageSize = 10)
        {
            using (NewsContext context = new NewsContext())
            {
                var result = from n in context.News
                             join c in context.Categories on n.CategoryId equals c.Id
                             join a in context.Authors on n.AuthorId equals a.Id
                             orderby n.PublishDate descending
                             select new NewsDetailDto
                             {
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

                return result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public List<NewsDetailDto> GetNewsDetailByCategoryId(int categoryId)
        {
            using (NewsContext context = new NewsContext())
            {
                var result = from n in context.News
                             join c in context.Categories
                                on n.CategoryId equals c.Id
                             join a in context.Authors
                                on n.AuthorId equals a.Id
                             where n.CategoryId == categoryId
                             select new NewsDetailDto
                             {
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
                return result.ToList();
            }
        }

    }
} 