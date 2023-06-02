using AspnetApp.Data.DbContexts;
using AspnetApp.Data.GenericRepositories;
using AspnetApp.Data.IGenericRepositories;
using AspnetApp.Domain.Entities;
using AspnetApp.Service.IServices.Answers;
using AspnetApp.Service.IServices.Attachments;
using AspnetApp.Service.IServices.Courses;
using AspnetApp.Service.IServices.Questions;
using AspnetApp.Service.IServices.Quizes;
using AspnetApp.Service.IServices.QuizResults;
using AspnetApp.Service.IServices.SolvedQuestions;
using AspnetApp.Service.IServices.Users;
using AspnetApp.Service.Services.Answers;
using AspnetApp.Service.Services.Attachments;
using AspnetApp.Service.Services.Courses;
using AspnetApp.Service.Services.Questions;
using AspnetApp.Service.Services.Quizes;
using AspnetApp.Service.Services.QuizResults;
using AspnetApp.Service.Services.SolvedQuestions;
using AspnetApp.Service.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AspnetApp.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCustomerService(this IServiceCollection services)
        {
            // GenericRepositories
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Course>, GenericRepository<Course>>();
            services.AddScoped<IGenericRepository<Quiz>, GenericRepository<Quiz>>();
            services.AddScoped<IGenericRepository<Question>, GenericRepository<Question>>();
            services.AddScoped<IGenericRepository<Attachment>, GenericRepository<Attachment>>();
            services.AddScoped<IGenericRepository<Answer>, GenericRepository<Answer>>();
            services.AddScoped<IGenericRepository<QuizRezult>, GenericRepository<QuizRezult>>();
            services.AddScoped<IGenericRepository<SolvedQuestion>, GenericRepository<SolvedQuestion>>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IQuizeService, QuizService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IQuizResultService, QuizResultService>();
            services.AddScoped<ISolvedQuestionService, SolvedQuestionService>();

        }
            public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
            {
                var jwtSettings = configuration.GetSection("Jwt");

                string key = jwtSettings.GetSection("Key").Value;

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))

                    };
                });
            }

            public static void AddSwaggerService(this IServiceCollection services)
            {
                services.AddSwaggerGen(p =>
                {
                    p.ResolveConflictingActions(ad => ad.FirstOrDefault());
                    p.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header
                    });

                    p.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                });
            }
        }
    }