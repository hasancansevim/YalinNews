using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Helpers;
using Entities.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace Business.Concrete
{
    public class NewsManager : INewsService
    {
        private const string NewsGetAllCacheKey = "news_getall";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(15);

        private readonly INewsDal _newsDal;
        private readonly ICategoryService _categoryService;
        private readonly IMemoryCache _memoryCache;

        public NewsManager(INewsDal newsDal, ICategoryService categoryService, IMemoryCache memoryCache)
        {
            _newsDal = newsDal;
            _categoryService = categoryService;
            _memoryCache = memoryCache;
        }

        //[SecuredOperation("admin,editor")]
        [ValidationAspect(typeof(NewsValidator))]
        //[CacheRemoveAspect("INewsService.Get")]
        public IResult Add(News news)
        {
            if (string.IsNullOrWhiteSpace(news.Slug))
            {
                news.Slug = SlugHelper.Generate(news.Title);
            }

            _newsDal.Add(news);
            InvalidateNewsCaches();
            return new SuccessResult(Messages.NewsAdded);
        }

        //[SecuredOperation("admin,editor")]
        public IResult Update(News news)
        {
            if (string.IsNullOrWhiteSpace(news.Slug))
            {
                news.Slug = SlugHelper.Generate(news.Title);
            }

            _newsDal.Update(news);
            InvalidateNewsCaches();
            return new SuccessResult(Messages.NewsUpdated);
        }

        //[SecuredOperation("admin")]
        public IResult Delete(News news)
        {
            _newsDal.Delete(news);
            InvalidateNewsCaches();
            return new SuccessResult(Messages.NewsDeleted);
        }

        public IDataResult<List<News>> GetAll()
        {
            if (_memoryCache.TryGetValue(NewsGetAllCacheKey, out List<News> cachedNews))
            {
                return new SuccessDataResult<List<News>>(cachedNews, Messages.NewsListed);
            }

            var result = _newsDal.GetAll();
            _memoryCache.Set(NewsGetAllCacheKey, result, CacheDuration);

            return new SuccessDataResult<List<News>>(result, Messages.NewsListed);
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
            var cacheKey = $"news_details_{page}_{pageSize}";

            if (_memoryCache.TryGetValue(cacheKey, out List<NewsDetailDto> cachedNews))
            {
                return new SuccessDataResult<List<NewsDetailDto>>(cachedNews);
            }

            var result = _newsDal.GetNewsDetail(page, pageSize);
            _memoryCache.Set(cacheKey, result, CacheDuration);

            return new SuccessDataResult<List<NewsDetailDto>>(result);
        }

        public IDataResult<List<SitemapNewsDto>> GetPublishedNewsForSitemap()
        {
            const string cacheKey = "news_sitemap";

            if (_memoryCache.TryGetValue(cacheKey, out List<SitemapNewsDto> cachedItems))
            {
                return new SuccessDataResult<List<SitemapNewsDto>>(cachedItems);
            }

            var result = _newsDal.GetPublishedNewsForSitemap();
            _memoryCache.Set(cacheKey, result, CacheDuration);

            return new SuccessDataResult<List<SitemapNewsDto>>(result);
        }

        private void InvalidateNewsCaches()
        {
            _memoryCache.Remove(NewsGetAllCacheKey);
            _memoryCache.Remove("news_sitemap");
            // Clear common paginated detail caches used by the frontend
            _memoryCache.Remove("news_details_1_50");
            _memoryCache.Remove("news_details_1_10");
        }
    }
}