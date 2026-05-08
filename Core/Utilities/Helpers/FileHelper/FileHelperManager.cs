using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FileHelperManager : IFileHelper
    {
        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"
        };

        public string Upload(IFormFile file, string root)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Yuklenecek dosya bulunamadi.");
            }

            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(extension) || !AllowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Sadece resim dosyalari yuklenebilir.");
            }

            var normalizedRoot = NormalizeRoot(root);
            if (!Directory.Exists(normalizedRoot))
            {
                Directory.CreateDirectory(normalizedRoot);
            }

            var fileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(normalizedRoot, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                file.CopyTo(stream);
            }

            var relativePath = Path.Combine(root, fileName).Replace("\\", "/");
            return relativePath;
        }

        public void Delete(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            var normalizedPath = filePath;
            if (!Path.IsPathRooted(normalizedPath))
            {
                normalizedPath = Path.Combine(Directory.GetCurrentDirectory(), normalizedPath);
            }

            if (File.Exists(normalizedPath))
            {
                File.Delete(normalizedPath);
            }
        }

        public string Update(IFormFile file, string filePath, string root)
        {
            Delete(filePath);
            return Upload(file, root);
        }

        private static string NormalizeRoot(string root)
        {
            if (string.IsNullOrWhiteSpace(root))
            {
                throw new ArgumentException("Kayit dizini bos olamaz.");
            }

            if (Path.IsPathRooted(root))
            {
                return root;
            }

            return Path.Combine(Directory.GetCurrentDirectory(), root);
        }
    }
}
