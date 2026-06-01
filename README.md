# Yalin News Backend API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)
![Swagger](https://img.shields.io/badge/-Swagger-%23Clojure?style=for-the-badge&logo=swagger&logoColor=white)

Yalin News Backend API, modern ve ölçeklenebilir bir haber portalı için geliştirilmiş RESTful servisidir. Proje, kurumsal yazılım geliştirme standartlarına ve SOLID prensiplerine tam uyumlu olarak, Onion/N-Tier mimarisi temel alınarak inşa edilmiştir.

## 🚀 Öne Çıkan Özellikler

- **Gelişmiş İçerik Yönetimi:** Haber, Kategori ve Yazar entitileri için tam kapsamlı CRUD operasyonları.
- **Güvenli Kimlik Doğrulama:** JWT (JSON Web Token) tabanlı güvenli oturum yönetimi ve yetkilendirme.
- **Rol Bazlı Erişim Kontrolü (RBAC):** Admin, Editor ve Standart Kullanıcı rolleri için endpoint bazlı erişim kısıtlamaları.
- **Genişletilebilir Mimari:** Generic Repository ve Unit of Work tasarım desenleri ile soyutlanmış veritabanı işlemleri.
- **AOP (Aspect Oriented Programming):** Loglama, validasyon, yetkilendirme ve önbellekleme (caching) gibi çapraz kesen ilgilerin (Cross-Cutting Concerns) merkezi yönetimi.
- **Esnek Yapılandırma:** Geliştirme (Local) ve Üretim (Production) ortamları için tamamen çevresel değişkenler (Environment Variables) üzerinden konfigürasyon yönetimi.
- **CORS Politikaları:** Sadece güvenilir frontend istemcilerine (`yalinnews.vercel.app`, `localhost:4200`) izin veren sıkı güvenlik önlemleri.

## 🛠️ Teknoloji Yığını

- **Framework:** .NET 8.0 (Web API)
- **Veritabanı & ORM:** PostgreSQL, Entity Framework Core 9.0
- **IoC Container:** Autofac 9.0
- **Kimlik Doğrulama:** Microsoft.AspNetCore.Authentication.JwtBearer
- **Dokümantasyon:** Swagger / OpenAPI
- **Konteynerleştirme:** Docker (Multi-stage build)

## 📦 Katmanlı Mimari (N-Tier Architecture)

Proje, bağımlılıkları minimize etmek ve sürdürülebilirliği artırmak için 5 ana katmandan oluşmaktadır:

1. **Core:** Tüm projelerde kullanılabilecek, bağımsız altyapı kodlarını (AOP interceptor'ları, JWT altyapısı, şifreleme, result tipleri) barındırır.
2. **Entities:** Veritabanı tablolarına karşılık gelen varlık sınıflarını (Concrete) ve DTO'ları içerir.
3. **DataAccess:** Veritabanı CRUD işlemleri ve Entity Framework yapılandırmalarını barındırır.
4. **Business:** İş kurallarının, validasyonların ve yetkilendirme mantığının işletildiği katmandır.
5. **WebAPI:** Dış dünyaya açılan kapıdır. Gelen HTTP isteklerini karşılar ve Business katmanına iletir.

## ⚙️ Çevresel Değişkenler (Environment Variables)

Projeyi ayağa kaldırmadan önce aşağıdaki çevresel değişkenlerin tanımlanması gerekmektedir. Örnek yapılandırma için `WebAPI/appsettings.example.json` dosyasını inceleyebilirsiniz.

| Değişken Adı | Açıklama |
|---|---|
| `DB_CONNECTION_STRING` | PostgreSQL veritabanı bağlantı dizesi |
| `JWT_SECRET_KEY` | JWT imzalaması için kullanılacak gizli anahtar |
| `JWT_AUDIENCE` | Token'ın hedef kitlesi |
| `JWT_ISSUER` | Token'ı oluşturan kurum/uygulama |
| `JWT_EXPIRATION` | Token geçerlilik süresi (dakika) |

## 🚀 Başlangıç & Kurulum

### Seçenek 1: Docker ile Çalıştırma (Önerilen)

Proje dizininde aşağıdaki komutu çalıştırarak uygulamayı anında ayağa kaldırabilirsiniz:

```bash
docker build -t yalinnews-api .
docker run -d -p 8080:8080 --env-file .env yalinnews-api
```

### Seçenek 2: Manuel Kurulum (.NET CLI)

1. Projeyi klonlayın:
```bash
git clone https://github.com/svmhasancan/YalinNews.git
cd YalinNews/Backend/YalinNews
```

2. `WebAPI` klasörüne gidin ve bağımlılıkları yükleyin:
```bash
cd WebAPI
dotnet restore
```

3. Veritabanı migrasyonlarını uygulayın (Veritabanının çalıştığından ve değişkenlerin ayarlandığından emin olun):
```bash
dotnet ef database update
```

4. Projeyi çalıştırın:
```bash
dotnet run
```

## 📚 API Dokümantasyonu

Uygulama çalıştıktan sonra Swagger arayüzü üzerinden tüm endpoint'leri test edebilirsiniz:
- **Lokal:** `http://localhost:<PORT>/swagger`

### Temel Endpoint Grupları
- `POST /api/auth/...` - Kayıt ve Giriş işlemleri
- `GET, POST, PUT, DELETE /api/news/...` - Haber yönetimi (Yetki gerektirir)
- `GET, POST /api/categories/...` - Kategori işlemleri
- `GET, POST /api/authors/...` - Yazar işlemleri

## ☁️ Dağıtım (Deployment)

Bu proje Render.com ve Railway gibi PaaS platformlarında çalışmaya tam uyumludur. `Dockerfile` multi-stage yapıdadır ve ortam değişkenleriyle (Environment Variables) dışarıdan konfigüre edilecek şekilde tasarlanmıştır. Projede hassas veriler kaynak koddan izole edilmiştir.

## 🤝 Katkıda Bulunma Standartları

1. **Fork** işlemi ile projeyi kendi hesabınıza kopyalayın.
2. Yeni özellik için **branch** oluşturun (`git checkout -b feature/YeniOzellik`).
3. Değişikliklerinizi **commit** edin (`git commit -m 'feat: Yeni özellik eklendi'`).
4. Branch'inize **push** yapın (`git push origin feature/YeniOzellik`).
5. **Pull Request (PR)** açarak kod incelemesi talep edin.
