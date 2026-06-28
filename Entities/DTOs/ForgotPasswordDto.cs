using Core.Entities;

namespace Entities.DTOs
{
    public class ForgotPasswordDto : IDto
    {
        public string Email { get; set; }
    }
}
