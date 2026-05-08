using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Entities.DTOs;


namespace Business.Concrete
{
    public class NewsManager : INewsService
    {
        private INewsDal _newsDal;
        private ICategoryService _categoryService;

        public NewsManager(INewsDal newsDal,ICategoryService categoryService)
        {
            _newsDal = newsDal;
            _categoryService = categoryService;
        }

        //[SecuredOperation("admin,editor")]
        [ValidationAspect(typeof(NewsValidator))]
        //[CacheRemoveAspect("INewsService.Get")]
        public IResult Add(News news)
        {
            _newsDal.Add(news);
            return new SuccessResult(Messages.NewsAdded);
        }

        [SecuredOperation("admin,editor")]
        public IResult Update(News news)
        {
            _newsDal.Update(news);
            return new SuccessResult(Messages.NewsUpdated);
        }

        //[SecuredOperation("admin")]
        public IResult Delete(News news)
        {
            _newsDal.Delete(news);
            return new SuccessResult(Messages.NewsDeleted);
        }

        [CacheAspect(duration: 10)]
        public IDataResult<List<News>> GetAll()
        {
            var result = _newsDal.GetAll();
            return new SuccessDataResult<List<News>>(result,Messages.NewsListed);
        }

        public IDataResult<List<News>> GetAllByAuthorId(int authorId)
        {
            var result = _newsDal.GetAll(n => n.AuthorId == authorId);
            return new SuccessDataResult<List<News>>(result, Messages.NewsListed);
        }

        public IDataResult<List<NewsDetailDto>> GetAllByCategoryId(int categoryId)
        {
            var result = _newsDal.GetNewsDetailByCategoryId(categoryId);
            return new SuccessDataResult<List<NewsDetailDto>>(result, Messages.NewsListed);
        }

        public IDataResult<int> GetCategoryIdByName(string categoryName)
        {
            var categoryId = _categoryService.GetByName(categoryName).Data.Id;

            return new SuccessDataResult<int>(categoryId);
        }

        public IDataResult<News> GetById(int newsId)
        {
            var news = _newsDal.Get(n => n.Id == newsId);
            if (news == null)
                return new ErrorDataResult<News>(Messages.NewsNotFound);
                
            return new SuccessDataResult<News>(news);
        }

        public IDataResult<List<NewsDetailDto>> GetNewsDetails(int page = 1, int pageSize = 10)
        {
            var result = _newsDal.GetNewsDetail(page, pageSize);
            return new SuccessDataResult<List<NewsDetailDto>>(result);
        }
    }
} 