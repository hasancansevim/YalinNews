using Core.Entities.Concrete;
using Core.Security.JWT;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        IResult ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}