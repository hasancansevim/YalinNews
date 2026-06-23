using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options) : base(options) { }
        public NewsContext() : base(
            new DbContextOptionsBuilder<NewsContext>()
                .UseNpgsql("Host=localhost;Database=dummy;Username=postgres;Password=dummy")
                .Options)
        {
        }

        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var publishDate = DateTime.UtcNow.Date;

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Teknoloji"
                }
            );

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "Hasan Can",
                    LastName = "Sevim",
                    Email = "hasan.can@yalinnews.com",
                    Biography = "Yapay zeka, girisimcilik ve urun teknolojileri uzerine yaziyor.",
                    ImageUrl = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e",
                    IsActive = true
                }
            );

            modelBuilder.Entity<News>().HasData(
                new News
                {
                    Id = 1,
                    Title = "Yapay Zeka Destekli Arama Motorlari Yeni Doneme Giriyor",
                    Content = "Yeni nesil arama motorlari, uretebilen yapay zeka ile kullanicilara daha dogrudan ve baglamsal cevaplar sunuyor. Sirketler bu alanda rekabeti hizlandirirken, dogruluk ve guvenilirlik odakli yeni standartlar gelistiriyor.",
                    ImageUrl = "https://images.unsplash.com/photo-1677442136019-21780ecad995",
                    PublishDate = publishDate,
                    CategoryId = 1,
                    AuthorId = 1,
                    Status = NewsStatus.Published,
                    SpotText = "Arama deneyimi, uretken yapay zeka ile kokten degisiyor.",
                    ViewCount = 1240
                },
                new News
                {
                    Id = 2,
                    Title = "Bulut Teknolojilerinde Maliyet Optimizasyonu Trendleri",
                    Content = "Kurumsal ekipler, bulut altyapisinda otomatik olcekleme ve kaynak gozlemlenebilirligi sayesinde giderlerini ciddi oranda azaltmayi hedefliyor. FinOps yaklasimi, teknoloji yonetiminde temel strateji haline geliyor.",
                    ImageUrl = "https://images.unsplash.com/photo-1451187580459-43490279c0fa",
                    PublishDate = publishDate,
                    CategoryId = 1,
                    AuthorId = 1,
                    Status = NewsStatus.Published,
                    SpotText = "FinOps yaklasimi teknoloji sirketlerinin gundeminde ilk sirada.",
                    ViewCount = 980
                },
                new News
                {
                    Id = 3,
                    Title = "Siber Guvenlikte Zero Trust Mimarisi Yayginlasiyor",
                    Content = "Siber tehditlerin artmasi, kurumlari sifir guven yaklasimina yoneltiyor. Kimlik dogrulama, ag segmentasyonu ve surekli izleme mekanizmalari modern guvenlik mimarisinin temeli olarak one cikiyor.",
                    ImageUrl = "https://images.unsplash.com/photo-1563986768609-322da13575f3",
                    PublishDate = publishDate,
                    CategoryId = 1,
                    AuthorId = 1,
                    Status = NewsStatus.Published,
                    SpotText = "Zero Trust, kurumlarin varsayilan guven anlayisini degistiriyor.",
                    ViewCount = 1125
                },
                new News
                {
                    Id = 4,
                    Title = "Elektrikli Arac Ekosistemi Icin Yeni Sarj Standartlari",
                    Content = "Avrupa ve Turkiye pazarinda hizli sarj istasyonlarinin uyumlulugunu artirmaya yonelik yeni standartlar hayata geciyor. Bu adim, elektrikli arac kullaniminda altyapi kaygilarini azaltmayi hedefliyor.",
                    ImageUrl = "https://images.unsplash.com/photo-1593941707882-a5bba14938c7",
                    PublishDate = publishDate,
                    CategoryId = 1,
                    AuthorId = 1,
                    Status = NewsStatus.Published,
                    SpotText = "Sarj altyapisinda standartlasma elektrikli arac yayginligini destekliyor.",
                    ViewCount = 860
                },
                new News
                {
                    Id = 5,
                    Title = "Acik Kaynak LLM Projeleri Girisimleri Hizlandiriyor",
                    Content = "Acik kaynak buyuk dil modeli projeleri, girisimlerin urun gelistirme surelerini kisaltirken maliyetleri de dusuruyor. Topluluk destegiyle guclenen ekosistem, yeni urun fikirlerinin daha hizli test edilmesini sagliyor.",
                    ImageUrl = "https://images.unsplash.com/photo-1518770660439-4636190af475",
                    PublishDate = publishDate,
                    CategoryId = 1,
                    AuthorId = 1,
                    Status = NewsStatus.Published,
                    SpotText = "Acik kaynak LLM dunyasi, urun inovasyonunu ivmelendiriyor.",
                    ViewCount = 1345
                }
            );
        }
    }
} 