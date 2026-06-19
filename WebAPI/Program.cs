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
builder.Services.AddMemoryCache();

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<NewsContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"],
    b => b.MigrationsAssembly("DataAccess")));


// CORS yapılandırması
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// JWT Authentication yapılandırması
var tokenOptions = new TokenOptions
{
    Audience = builder.Configuration["TokenOptions:Audience"],
    Issuer = builder.Configuration["TokenOptions:Issuer"],
    AccessTokenExpiration = int.TryParse(builder.Configuration["TokenOptions:AccessTokenExpiration"], out var exp) ? exp : 60,
    SecurityKey = builder.Configuration["TokenOptions:SecurityKey"]
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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NewsContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();