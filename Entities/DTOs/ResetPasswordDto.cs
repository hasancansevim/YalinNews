using Core.Entities;

namespace Entities.DTOs
{
    public class ResetPasswordDto : IDto
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
