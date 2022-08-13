using FluentValidation;
using Main.API.DtoModels;
using Main.API.Services;
using Main.API.Services.Interfaces;
using Main.API.Validators;
using Microsoft.OpenApi.Models;

namespace Main.API.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
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

    public static void AddValidators(this IServiceCollection services) 
    {
        services.AddScoped<IValidator<ArticleDto>, ArticleDtoValidator>();
        services.AddScoped<IValidator<ArticleForCreationDto>, ArticleForCreationDtoValidator>();
        services.AddScoped<IValidator<ArticleForUpdateDto>, ArticleForUpdateDtoValidator>();
    } 
}