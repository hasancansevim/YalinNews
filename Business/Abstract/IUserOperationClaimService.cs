using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationClaim userOperationClaim);
        IResult Update(UserOperationClaim userOperationClaim);
        IResult Delete(UserOperationClaim userOperationClaim);
        IDataResult<List<UserOperationClaim>> GetAll();
        IDataResult<UserOperationClaim> GetById(int id);
        IDataResult<List<UserOperationClaim>> GetByUserId(int userId);
    }
}
