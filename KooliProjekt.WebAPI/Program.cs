using System;
using System.Text;
using FluentValidation;
using KooliProjekt.Application.Authentication.Jwt;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure;
using KooliProjekt.Application.Infrastructure.Files;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace KooliProjekt.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ---------- JWT autentimine (20.11) ----------
            var jwtSection = builder.Configuration.GetSection("Jwt");
            if (!jwtSection.Exists())
            {
                throw new InvalidOperationException("JWT configuration section is missing in appsettings.json.");
            }
            var jwtKey = Encoding.UTF8.GetBytes(jwtSection["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSection["Audience"],
                    ClockSkew = TimeSpan.Zero,
                };
            });

            var jwtSettings = new JwtSettings(builder.Configuration);
            builder.Services.AddSingleton(jwtSettings);
            builder.Services.AddSingleton<JwtTokenBuilder>();
            builder.Services.AddHttpContextAccessor();   // vajalik WebUserProvider'ile (ülesanne 9)
            builder.Services.AddScoped<IUserProvider, WebUserProvider>();
            builder.Services.AddAuthorization();

            var filesRoot = builder.Configuration["FilesRoot"]
                ?? throw new InvalidOperationException("FilesRoot is not configured in appsettings.json");
            builder.Services.AddSingleton<IFileClient>(new LocalFileClient(filesRoot));

            var applicationAssembly = typeof(ErrorHandlingBehavior<,>).Assembly;
            builder.Services.AddValidatorsFromAssembly(applicationAssembly);
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(applicationAssembly);
                config.AddOpenBehavior(typeof(ErrorHandlingBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(TransactionalBehavior<,>));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Järjekord on oluline: enne tuvastame kes sa oled, siis kontrollime mida tohid
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                if (!builder.Configuration.GetValue<bool>("SkipAutomaticMigrations"))
                {
                    dbContext.Database.Migrate();
                }

#if DEBUG
                SeedData.Generate(dbContext);
#endif
            }

            app.Run();

        }
    }
}
