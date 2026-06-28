using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Security.Hashing;
using Core.Security.JWT;
using Core.Utilities.Results;
using Entities.DTOs;

using Microsoft.Extensions.Caching.Memory;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IEmailService _emailService;
        private IMemoryCache _memoryCache;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IEmailService emailService, IMemoryCache memoryCache)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _emailService = emailService;
            _memoryCache = memoryCache;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);

            return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (!userToCheck.Success)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, 
                userToCheck.Data.PasswordHash, 
                userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            var userToCheck = _userService.GetByMail(userForRegisterDto.Email).Data;

            if(userToCheck != null)
            {
                return new ErrorDataResult<User>(Messages.UserAlreadyExists);
            }

            HashingHelper.CreatePasswordHash(userForRegisterDto.Password,out passwordHash,out passwordSalt);

            var user = new User
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.Add(user);

            return new SuccessDataResult<User>(user,Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            var userToCheck = _userService.GetByMail(email).Data;

            if(userToCheck == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            return new SuccessResult(Messages.UserAlreadyExists);
        }

        public IResult ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = _userService.GetByMail(forgotPasswordDto.Email).Data;
            if (user == null)
            {
                // Return success even if user not found to prevent email enumeration
                return new SuccessResult("Eğer sistemimizde kayıtlı bir e-posta adresi girdiyseniz, şifre sıfırlama bağlantısı gönderilmiştir.");
            }

            var resetToken = Guid.NewGuid().ToString("N");
            _memoryCache.Set(resetToken, user.Email, TimeSpan.FromMinutes(15));

            var resetLink = $"https://yalinnews.com/reset-password?token={resetToken}";
            
            // LİNKİ LOGLARA YAZDIRIYORUZ (E-posta gitmese bile Render loglarından alınıp test edilebilsin diye)
            Console.WriteLine("=================================================");
            Console.WriteLine($"[DİKKAT] ŞİFRE SIFIRLAMA LİNKİ: {resetLink}");
            Console.WriteLine("=================================================");

            var emailBody = $@"
                <h3>Şifre Sıfırlama Talebi</h3>
                <p>YalınNews hesabınızın şifresini sıfırlamak için aşağıdaki bağlantıya tıklayın:</p>
                <p><a href='{resetLink}'>{resetLink}</a></p>
                <p>Bu bağlantı 15 dakika süreyle geçerlidir.</p>";

            _emailService.SendEmail(user.Email, "YalınNews Şifre Sıfırlama", emailBody);

            return new SuccessResult("Eğer sistemimizde kayıtlı bir e-posta adresi girdiyseniz, şifre sıfırlama bağlantısı gönderilmiştir.");
        }

        public IResult ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!_memoryCache.TryGetValue(resetPasswordDto.Token, out string email))
            {
                return new ErrorResult("Geçersiz veya süresi dolmuş bir şifre sıfırlama bağlantısı kullandınız.");
            }

            var user = _userService.GetByMail(email).Data;
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(resetPasswordDto.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userService.Update(user);
            _memoryCache.Remove(resetPasswordDto.Token);

            return new SuccessResult("Şifreniz başarıyla güncellendi.");
        }
    }
} 