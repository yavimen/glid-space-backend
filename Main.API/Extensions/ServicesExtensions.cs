using FluentValidation;
using Main.API.DtoModels;
using Main.API.EmailService;
using Main.API.Persistance;
using Main.API.Services;
using Main.API.Services.Interfaces;
using Main.API.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace Main.API.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
    
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.IncludeXmlComments("SwaggerDoc.xml");
            
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Main API",
                Version = "v1"
            });

            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
                
            c.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
            c.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services) 
    {
        services.AddScoped<IValidator<ArticleDto>, ArticleDtoValidator>();
        services.AddScoped<IValidator<ArticleForCreationDto>, ArticleForCreationDtoValidator>();
        services.AddScoped<IValidator<ArticleForUpdateDto>, ArticleForUpdateDtoValidator>();
        services.AddScoped<IValidator<UserRegistrationDto>, UserRegistrationDtoValidator>();
        return services;
    }

    public static IServiceCollection ConfigureIdentity(this IServiceCollection services) 
    {
        services.AddIdentity<User, IdentityRole>(opt => {
            opt.Password.RequiredLength = 8;
            opt.Password.RequireDigit = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireNonAlphanumeric = false; 

            opt.User.RequireUniqueEmail = true;

            opt.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<MainDbContext>()
        .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(2));
        return services;
    }

    public static IServiceCollection ConfigureEmailService(this IServiceCollection services, 
        ConfigurationManager configuration) 
    {
        services.AddSingleton<IEmailSender, EmailSender>();

        var emailConfig = configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();

        services.AddSingleton(emailConfig);


        return services;
    }
}