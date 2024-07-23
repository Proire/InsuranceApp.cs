using InsuranceAppBLL.AdminService;
using InsuranceAppRLL;
using InsuranceAppRLL.CQRS.Handlers.AdminHandlers;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Implementations.AdminRepository;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using InsuranceAppRLL.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using System.Text;
using UserRLL.Utilities;

namespace InsuranceApp.cs
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Logging
            builder.Services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); // Set minimum log level

                // Add NLog as the logging provider
                logging.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddDbContext<InsuranceDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("InsuranceDbConnection")));

            // Register Repositories
            builder.Services.AddScoped<IAdminCommandRepository, AdminCommandRepository>();
            builder.Services.AddScoped<IAdminQueryRepository, AdminQueryRepository>();

            // Business layer services
            builder.Services.AddScoped<IAdminService,AdminService>();

            // Mediator service
            builder.Services.AddMediatR(typeof(Program).Assembly);
            builder.Services.AddMediatR(typeof(InsertAdminCommandHandler).Assembly);

            // utility services
            builder.Services.AddScoped<JwtTokenGenerator>();
            builder.Services.AddScoped<RabitMQProducer>();

            // JWT Configurations
            // Add Appsettings Configuration Builder 
            builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Getting Values from AppSettings.json
            var config = builder.Configuration;
            var secretKey = Environment.GetEnvironmentVariable("SecretKey");
            var issuer = config["Jwt:ValidIssuer"];
            var audience = config["Jwt:ValidAudience"];

            builder.Services.AddAuthentication().AddJwtBearer("AdminScheme", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? throw new Exception("Provide Secret Key")))
                };
            })
            .AddJwtBearer("UserScheme", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? throw new Exception("Provide Secret Key")))
                };
            })
            .AddJwtBearer("UserValidationScheme", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? throw new Exception("Provide Secret Key")))
                };
            })
            .AddJwtBearer("EmailVerificationScheme", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? throw new Exception("Provide Secret Key")))
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}