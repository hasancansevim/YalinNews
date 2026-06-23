using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IFavoriteService
    {
        IResult Add(Favorite favorite);
        IResult Delete(Favorite favorite);
        IDataResult<List<NewsDetailDto>> GetAllByUserId(int userId);
        IDataResult<List<Favorite>> GetAllByNewsId(int newsId);
    }
}
