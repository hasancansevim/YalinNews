using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialSupabaseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "SpotText",
                table: "News",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "News",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "News",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "ImageUrl", "PublishDate", "SpotText", "Status", "Title", "ViewCount" },
                values: new object[,]
                {
                    { 1, 1, 1, "Yeni nesil arama motorlari, uretebilen yapay zeka ile kullanicilara daha dogrudan ve baglamsal cevaplar sunuyor. Sirketler bu alanda rekabeti hizlandirirken, dogruluk ve guvenilirlik odakli yeni standartlar gelistiriyor.", "https://images.unsplash.com/photo-1677442136019-21780ecad995", new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Arama deneyimi, uretken yapay zeka ile kokten degisiyor.", 2, "Yapay Zeka Destekli Arama Motorlari Yeni Doneme Giriyor", 1240 },
                    { 2, 1, 1, "Kurumsal ekipler, bulut altyapisinda otomatik olcekleme ve kaynak gozlemlenebilirligi sayesinde giderlerini ciddi oranda azaltmayi hedefliyor. FinOps yaklasimi, teknoloji yonetiminde temel strateji haline geliyor.", "https://images.unsplash.com/photo-1451187580459-43490279c0fa", new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), "FinOps yaklasimi teknoloji sirketlerinin gundeminde ilk sirada.", 2, "Bulut Teknolojilerinde Maliyet Optimizasyonu Trendleri", 980 },
                    { 3, 1, 1, "Siber tehditlerin artmasi, kurumlari sifir guven yaklasimina yoneltiyor. Kimlik dogrulama, ag segmentasyonu ve surekli izleme mekanizmalari modern guvenlik mimarisinin temeli olarak one cikiyor.", "https://images.unsplash.com/photo-1563986768609-322da13575f3", new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Zero Trust, kurumlarin varsayilan guven anlayisini degistiriyor.", 2, "Siber Guvenlikte Zero Trust Mimarisi Yayginlasiyor", 1125 },
                    { 4, 1, 1, "Avrupa ve Turkiye pazarinda hizli sarj istasyonlarinin uyumlulugunu artirmaya yonelik yeni standartlar hayata geciyor. Bu adim, elektrikli arac kullaniminda altyapi kaygilarini azaltmayi hedefliyor.", "https://images.unsplash.com/photo-1593941707882-a5bba14938c7", new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Sarj altyapisinda standartlasma elektrikli arac yayginligini destekliyor.", 2, "Elektrikli Arac Ekosistemi Icin Yeni Sarj Standartlari", 860 },
                    { 5, 1, 1, "Acik kaynak buyuk dil modeli projeleri, girisimlerin urun gelistirme surelerini kisaltirken maliyetleri de dusuruyor. Topluluk destegiyle guclenen ekosistem, yeni urun fikirlerinin daha hizli test edilmesini sagliyor.", "https://images.unsplash.com/photo-1518770660439-4636190af475", new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Acik kaynak LLM dunyasi, urun inovasyonunu ivmelendiriyor.", 2, "Acik Kaynak LLM Projeleri Girisimleri Hizlandiriyor", 1345 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "SpotText",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "News");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "News",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
