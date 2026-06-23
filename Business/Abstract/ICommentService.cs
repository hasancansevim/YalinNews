using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICommentService
    {
        IResult Add(Comment comment);
        IResult Delete(Comment comment);
        IResult Update(Comment comment);
        IDataResult<List<Comment>> GetAll();
        IDataResult<List<CommentDetailDto>> GetAllByNewsId(int newsId);
        IDataResult<Comment> GetById(int id);
    }
}
