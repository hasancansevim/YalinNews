using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class FavoriteManager : IFavoriteService
    {
        private IFavoriteDal _favoriteDal;

        public FavoriteManager(IFavoriteDal favoriteDal)
        {
            _favoriteDal = favoriteDal;
        }

        public IResult Add(Favorite favorite)
        {
            _favoriteDal.Add(favorite);
            return new SuccessResult("Favorilere eklendi.");
        }

        public IResult Delete(Favorite favorite)
        {
            _favoriteDal.Delete(favorite);
            return new SuccessResult("Favorilerden çıkarıldı.");
        }

        public IDataResult<List<Favorite>> GetAllByNewsId(int newsId)
        {
            return new SuccessDataResult<List<Favorite>>(_favoriteDal.GetAll(f => f.NewsId == newsId).ToList());
        }

        public IDataResult<List<Favorite>> GetAllByUserId(int userId)
        {
            return new SuccessDataResult<List<Favorite>>(_favoriteDal.GetAll(f => f.UserId == userId).ToList());
        }
    }
}
