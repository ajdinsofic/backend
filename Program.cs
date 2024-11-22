using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VolanGo.DbConnection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VolanGo;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Validacija izdavača i korisnika
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            // Izdavač i publika mogu biti specifični za tvoju aplikaciju
            ValidIssuer = builder.Configuration[JWTokenInformation.Issuer],   // Odredi izdavača
            ValidAudience = builder.Configuration[  JWTokenInformation.Audience], // Odredi publiku

            // Tajni ključ za verifikaciju JWT tokena
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTokenInformation.SecretKey))
        };
    });

// Dodavanje servisa
builder.Services.AddControllers();

// Swagger konfiguracija
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "VolanGO API",
        Version = "v1",
        Description = "API documentation for VolanGO project",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "your.email@example.com",
        }
    });
});

// Konfiguracija baze podataka


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("db1")));

var app = builder.Build();


// Swagger UI za razvojno okruženje
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("http://localhost:5136/swagger/v1/swagger.json", "VolanGO API v1");
        c.RoutePrefix = string.Empty; // Swagger UI dostupan na root URL-u
    });
}

// Postavljanje CORS politike pre Authorization-a
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();
app.MapControllers();

app.Run();
