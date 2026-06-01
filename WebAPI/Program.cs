using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Security.Encryption;
using Core.Security.JWT;
using Core.Utilities.IoC;
using Core.DependencyResolvers;
using Core.Extensions;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Autofac configuration
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    builder.RegisterModule(new AutofacBusinessModule()));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<NewsContext>(options =>
    options.UseNpgsql(builder.Configuration["DB_CONNECTION_STRING"],
    b => b.MigrationsAssembly("WebAPI")));


// CORS yapılandırması
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://yalinnews.vercel.app", "http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// JWT Authentication yapılandırması
var tokenOptions = new TokenOptions
{
    Audience = builder.Configuration["JWT_AUDIENCE"],
    Issuer = builder.Configuration["JWT_ISSUER"],
    AccessTokenExpiration = int.TryParse(builder.Configuration["JWT_EXPIRATION"], out var exp) ? exp : 60,
    SecurityKey = builder.Configuration["JWT_SECRET_KEY"]
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

builder.Services.AddDependencyResolvers(new ICoreModule[] {
    new CoreModule()
});

ServiceTool.Create(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// CORS middleware'ini en başa al
app.UseCors("AllowFrontend");

// app.UseHttpsRedirection();
app.UseStaticFiles();

// Authentication ve Authorization middleware'lerinin sırası önemli
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();