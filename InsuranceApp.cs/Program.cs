using InsuranceAppBLL.AdminService;
using InsuranceAppBLL.CommissionService;
using InsuranceAppBLL.CustomerService;
using InsuranceAppBLL.EmployeeService;
using InsuranceAppBLL.InsuranceAgentService;
using InsuranceAppBLL.InsurancePlanService;
using InsuranceAppBLL.LoginService;
using InsuranceAppBLL.PaymentService;
using InsuranceAppBLL.PolicyService;
using InsuranceAppBLL.SchemeService;
using InsuranceAppRLL;
using InsuranceAppRLL.CQRS.Handlers.AdminHandlers;
using InsuranceAppRLL.Entities;
using InsuranceAppRLL.Repositories.Implementations;
using InsuranceAppRLL.Repositories.Implementations.AdminRepository;
using InsuranceAppRLL.Repositories.Implementations.CommissionRepository;
using InsuranceAppRLL.Repositories.Implementations.CustomerRepository;
using InsuranceAppRLL.Repositories.Implementations.EmployeeRepository;
using InsuranceAppRLL.Repositories.Implementations.EmployeeSchemeRepository;
using InsuranceAppRLL.Repositories.Implementations.InsuranceAgentRepository;
using InsuranceAppRLL.Repositories.Implementations.InsurancePlanRepository;
using InsuranceAppRLL.Repositories.Implementations.PaymentRepository;
using InsuranceAppRLL.Repositories.Implementations.PolicyRepository;
using InsuranceAppRLL.Repositories.Implementations.SchemeRepository;
using InsuranceAppRLL.Repositories.Interfaces;
using InsuranceAppRLL.Repositories.Interfaces.AdminRepository;
using InsuranceAppRLL.Repositories.Interfaces.CommissionRepository;
using InsuranceAppRLL.Repositories.Interfaces.CustomerRepository;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeRepository;
using InsuranceAppRLL.Repositories.Interfaces.EmployeeSchemeRepository;
using InsuranceAppRLL.Repositories.Interfaces.InsuranceAgentRepository;
using InsuranceAppRLL.Repositories.Interfaces.InsurancePlanRepository;
using InsuranceAppRLL.Repositories.Interfaces.PaymentRepository;
using InsuranceAppRLL.Repositories.Interfaces.PolicyRepository;
using InsuranceAppRLL.Repositories.Interfaces.SchemeRepository;
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
            //builder.Services.AddLogging(logging =>
            //{
            //    logging.ClearProviders();
            //    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); // Set minimum log level

            //    // Add NLog as the logging provider
            //    logging.AddNLog(new NLogProviderOptions
            //    {
            //        CaptureMessageTemplates = true,
            //        CaptureMessageProperties = true
            //    });
            //});

            // CORS Policy
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("*")  // Allow any origin for testing purposes
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                                  });
            });

            builder.Services.AddControllers();
            builder.Services.AddDbContext<InsuranceDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("InsuranceDbConnection") ?? string.Empty));

            // Register Repositories
            builder.Services.AddScoped<IAdminCommandRepository, AdminCommandRepository>();
            builder.Services.AddScoped<IAdminQueryRepository, AdminQueryRepository>();  

            builder.Services.AddScoped<ILoginRepository, LoginRepository>();

            builder.Services.AddScoped<IEmployeeCommandRepository,EmployeeCommandRepository>();
            builder.Services.AddScoped<IEmployeeQueryRepository, EmployeeQueryRepository>();

            builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();    
            builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>(); 

            builder.Services.AddScoped<IInsuranceAgentCommandRepository, InsuranceAgentCommandRepository>();  
            builder.Services.AddScoped<IInsuranceAgentQueryRepository, InsuranceAgentQueryRepository>();

            builder.Services.AddScoped<IInsurancePlanCommandRepository, InsurancePlanCommandRepository>();
            builder.Services.AddScoped<IInsurancePlanQueryRepository, InsurancePlanQueryRepository>();

            builder.Services.AddScoped<IPolicyCommandRepository, PolicyCommandRepository>();
            builder.Services.AddScoped<IPolicyQueryRepository, PolicyQueryRepository>();

            builder.Services.AddScoped<ISchemeCommandRepository, SchemeCommandRepository>();
            builder.Services.AddScoped<ISchemeQueryRepository, SchemeQueryRepository>();  
            
            builder.Services.AddScoped<IEmployeeSchemeCommandRepository, EmployeeSchemeCommandRepository>();

            builder.Services.AddScoped<IPaymentCommandRepository, PaymentCommandRepository>();
            builder.Services.AddScoped<IPaymentQueryRepository, PaymentQueryRepository>();

            builder.Services.AddScoped<ICommissionQueryRepository, CommissionQueryRepository>();

            // Business layer services
            builder.Services.AddScoped<IAdminService,AdminService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>(); 
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IInsuranceAgentService, InsuranceAgentService>();   
            builder.Services.AddScoped<IInsurancePlanService, InsurancePlanService>();  
            builder.Services.AddScoped<ILoginService, LoginService>(); 
            builder.Services.AddScoped<ISchemeService, SchemeService>();
            builder.Services.AddScoped<IPolicyService, PolicyService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();  
            builder.Services.AddScoped<ICommissionService, CommissionService>();

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

            builder.Services.AddAuthentication()
            .AddJwtBearer("AdminScheme", options =>
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
            .AddJwtBearer("CustomerScheme", options =>
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
            .AddJwtBearer("InsuranceAgentScheme", options =>
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
            .AddJwtBearer("EmployeeScheme", options =>
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
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}