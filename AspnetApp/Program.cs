using AspnetApp.Data.DbContexts;
using AspnetApp.Domain.Enums;
using AspnetApp.Extensions;
using AspnetApp.Middlewares;
using AspnetApp.Service.Exceptions;
using AspnetApp.Service.Helpers;
using AspnetApp.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

        builder.Services.AddDbContext<AspnetAppDbContext>(option =>
        option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"), b => b.MigrationsAssembly("AspnetApp")));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCustomerService();
        builder.Services.ConfigureJwt(builder.Configuration);
        builder.Services.AddSwaggerService();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AllPolicy", policy => policy.RequireRole(
                Enum.GetName(UserRole.Admin),
                Enum.GetName(UserRole.User)));

            options.AddPolicy("AdminPolicy", policy => policy.RequireRole(
                Enum.GetName(UserRole.Admin)));

            options.AddPolicy("TecherPolicy", policy => policy.RequireRole(
                Enum.GetName(UserRole.Teacher)));
        });

        // Serilog
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        builder.Services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            EnvironmentHelper.WebRootPath = app.Services.GetRequiredService<IWebHostEnvironment>()?.WebRootPath;

            app.UseMiddleware<AspnetAppExceptionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}