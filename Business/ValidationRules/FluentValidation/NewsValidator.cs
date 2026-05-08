using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class NewsValidator : AbstractValidator<News>
    {
        public NewsValidator()
        {
            RuleFor(n => n.Title)
                .NotEmpty().WithMessage("Haber başlığı boş olamaz")
                .MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalıdır")
                .MaximumLength(300).WithMessage("Başlık en fazla 300 karakter olabilir");

            RuleFor(n => n.Content)
                .NotEmpty().WithMessage("Haber içeriği boş olamaz")
                .MinimumLength(10).WithMessage("İçerik en az 10 karakter olmalıdır");

            RuleFor(n => n.CategoryId)
                .NotEmpty().WithMessage("Kategori seçilmelidir")
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmelidir");

            RuleFor(n => n.AuthorId)
                .NotEmpty().WithMessage("Yazar seçilmelidir")
                .GreaterThan(0).WithMessage("Geçerli bir yazar seçilmelidir");

            RuleFor(n => n.PublishDate)
                .NotEmpty().WithMessage("Yayın tarihi boş olamaz");

            RuleFor(n => n.SpotText)
                .MaximumLength(500).WithMessage("Spot metni en fazla 500 karakter olabilir");

            RuleFor(n => n.Status)
                .IsInEnum().WithMessage("Haber durumu boş olamaz");
        }
    }
} 