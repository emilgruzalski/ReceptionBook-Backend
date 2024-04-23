using Microsoft.EntityFrameworkCore;
using Contracts;
using LoggerService;
using Repository;
using Service;
using Service.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ReceptionBook.Extensions
{
    public static class ServiceExtensions
    {
        // Configure CORS (Cross-Origin Resource Sharing) policies to allow interactions between different origins.
        public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin() // Allow access from any origin.
            .AllowAnyMethod() // Allow any HTTP method.
            .AllowAnyHeader() // Allow any header.
            .WithExposedHeaders("X-Pagination")); // Expose specific headers.
        });

        // Configure IIS integration for better hosting compatibility and performance when hosted on IIS.
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
                // Options can be set here for more advanced configurations.
            });

        // Configure logging service to use a singleton instance of LoggerManager throughout the application.
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        // Configure the Repository Manager to be available as a scoped service, which will handle data operations.
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        // Configure the Service Manager which will provide access to application-specific services as scoped instances.
        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        // Configure the database context using PostgreSQL with EF Core.
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseNpgsql(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("ReceptionBook")));

        // Configure ASP.NET Core Identity for handling user authentication and authorization.
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                // Password and user settings can be customized here.
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>() // Connect Identity to the EF Core database context.
            .AddDefaultTokenProviders(); // Add support for generating tokens for things like password reset.
        }

        // Configure JWT authentication to secure API endpoints using JSON Web Tokens.
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings"); // Retrieve JWT settings from the configuration.
            var secretKey = "ReceptionBookSecretKey123456789000"; // This should ideally be stored securely and not hard-coded.

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["validIssuer"], // Set the valid issuer from settings.
                    ValidAudience = jwtSettings["validAudience"], // Set the valid audience from settings.
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) // The signing key must match the key used to generate the token.
                };
            });
        }
    }
}
