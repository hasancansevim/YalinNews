using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface INewsService
    {
        IDataResult<List<News>> GetAll();
        IDataResult<News> GetById(int newsId);
        IDataResult<List<NewsDetailDto>> GetNewsDetails(int page = 1, int pageSize = 10);
        IDataResult<List<NewsDetailDto>> GetAllByCategoryId(int categoryId);
        IDataResult<List<News>> GetAllByAuthorId(int authorId);
        IDataResult<int> GetCategoryIdByName(string categoryName);
        IResult Add(News news);
        IResult Update(News news);
        IResult Delete(News news);
    }
} 