using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface INewsDal : IEntityRepository<News>
    {
        List<NewsDetailDto> GetNewsDetail(int page = 1, int pageSize = 10);
        List<NewsDetailDto> GetNewsDetailByCategoryId(int categoryId);
    }
} 