# Railway Deployment Environment Variables

Uygulamayı Railway veya benzeri bir platforma deploy ederken aşağıdaki environment variable'ları (ortam değişkenlerini) tanımlamanız gerekmektedir. Güvenlik gereği hiçbir değer kod içinde tutulmamakta, bu değişkenler üzerinden okunmaktadır.

- `DB_CONNECTION_STRING` → PostgreSQL veya kullanacağınız veritabanı bağlantı dizesi (Connection String)
- `JWT_SECRET_KEY` → JWT token imzalamak için kullanılan gizli anahtar (min. 64 karakter olmalı)
- `JWT_AUDIENCE` → JWT token'ın hangi hedef kitle (audience) için oluşturulduğunu belirten değer (Örn: yalinnews.com)
- `JWT_ISSUER` → JWT token'ı oluşturan taraf (issuer) (Örn: yalinnews.com)
- `JWT_EXPIRATION` → Token geçerlilik süresi (dakika cinsinden, varsayılan 60 önerilir)
