using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Biography = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NewsId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NewsId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Slug = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SpotText = table.Column<string>(type: "text", nullable: true),
                    ViewCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OperationClaimId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "Email", "FirstName", "ImageUrl", "IsActive", "LastName" },
                values: new object[] { 1, "Yapay zeka, girisimcilik ve urun teknolojileri uzerine yaziyor.", "hasan.can@yalinnews.com", "Hasan Can", "https://images.unsplash.com/photo-1500648767791-00dcc994a43e", true, "Sevim" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Teknoloji" });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "ImageUrl", "PublishDate", "Slug", "SpotText", "Status", "Title", "ViewCount" },
                values: new object[,]
                {
                    { 1, 1, 1, "Yeni nesil arama motorlari, uretebilen yapay zeka ile kullanicilara daha dogrudan ve baglamsal cevaplar sunuyor. Sirketler bu alanda rekabeti hizlandirirken, dogruluk ve guvenilirlik odakli yeni standartlar gelistiriyor.", "https://images.unsplash.com/photo-1677442136019-21780ecad995", new DateTime(2026, 6, 19, 0, 0, 0, 0, DateTimeKind.Utc), null, "Arama deneyimi, uretken yapay zeka ile kokten degisiyor.", 2, "Yapay Zeka Destekli Arama Motorlari Yeni Doneme Giriyor", 1240 },
                    { 2, 1, 1, "Kurumsal ekipler, bulut altyapisinda otomatik olcekleme ve kaynak gozlemlenebilirligi sayesinde giderlerini ciddi oranda azaltmayi hedefliyor. FinOps yaklasimi, teknoloji yonetiminde temel strateji haline geliyor.", "https://images.unsplash.com/photo-1451187580459-43490279c0fa", new DateTime(2026, 6, 19, 0, 0, 0, 0, DateTimeKind.Utc), null, "FinOps yaklasimi teknoloji sirketlerinin gundeminde ilk sirada.", 2, "Bulut Teknolojilerinde Maliyet Optimizasyonu Trendleri", 980 },
                    { 3, 1, 1, "Siber tehditlerin artmasi, kurumlari sifir guven yaklasimina yoneltiyor. Kimlik dogrulama, ag segmentasyonu ve surekli izleme mekanizmalari modern guvenlik mimarisinin temeli olarak one cikiyor.", "https://images.unsplash.com/photo-1563986768609-322da13575f3", new DateTime(2026, 6, 19, 0, 0, 0, 0, DateTimeKind.Utc), null, "Zero Trust, kurumlarin varsayilan guven anlayisini degistiriyor.", 2, "Siber Guvenlikte Zero Trust Mimarisi Yayginlasiyor", 1125 },
                    { 4, 1, 1, "Avrupa ve Turkiye pazarinda hizli sarj istasyonlarinin uyumlulugunu artirmaya yonelik yeni standartlar hayata geciyor. Bu adim, elektrikli arac kullaniminda altyapi kaygilarini azaltmayi hedefliyor.", "https://images.unsplash.com/photo-1593941707882-a5bba14938c7", new DateTime(2026, 6, 19, 0, 0, 0, 0, DateTimeKind.Utc), null, "Sarj altyapisinda standartlasma elektrikli arac yayginligini destekliyor.", 2, "Elektrikli Arac Ekosistemi Icin Yeni Sarj Standartlari", 860 },
                    { 5, 1, 1, "Acik kaynak buyuk dil modeli projeleri, girisimlerin urun gelistirme surelerini kisaltirken maliyetleri de dusuruyor. Topluluk destegiyle guclenen ekosistem, yeni urun fikirlerinin daha hizli test edilmesini sagliyor.", "https://images.unsplash.com/photo-1518770660439-4636190af475", new DateTime(2026, 6, 19, 0, 0, 0, 0, DateTimeKind.Utc), null, "Acik kaynak LLM dunyasi, urun inovasyonunu ivmelendiriyor.", 2, "Acik Kaynak LLM Projeleri Girisimleri Hizlandiriyor", 1345 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
