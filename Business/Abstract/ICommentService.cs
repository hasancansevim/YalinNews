using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICommentService
    {
        IResult Add(Comment comment);
        IResult Delete(Comment comment);
        IResult Update(Comment comment);
        IDataResult<List<Comment>> GetAll();
        IDataResult<List<Comment>> GetAllByNewsId(int newsId);
        IDataResult<Comment> GetById(int id);
    }
}
