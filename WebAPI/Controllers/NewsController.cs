using Business.Abstract;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IFileHelper _fileHelper;

        public NewsController(INewsService newsService, IFileHelper fileHelper)
        {
            _newsService = newsService;
            _fileHelper = fileHelper;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _newsService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _newsService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbycategoryid")]
        public IActionResult GetByCategoryId(int id)
        {
            var result = _newsService.GetAllByCategoryId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyauthor")]
        public IActionResult GetByAuthor(int authorId)
        {
            var result = _newsService.GetAllByAuthorId(authorId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getnewsbydetails")]
        public IActionResult GetNewsByDetails([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = _newsService.GetNewsDetails(page, pageSize);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(News news)
        {
            var result = _newsService.Add(news);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(News news)
        {
            var result = _newsService.Update(news);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(News news)
        {
            var result = _newsService.Delete(news);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("uploadimage")]
        public IActionResult UploadImage(IFormFile file)
        {
            var filePath = _fileHelper.Upload(file, "wwwroot/uploads/images/");
            return Ok(new SuccessDataResult<string>(filePath, "Resim yuklendi."));
        }
    }
} 