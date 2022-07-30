using Main.API.Persistance;
using Main.API.Services;
using Main.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Main.API.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
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

    //public static void 

    public static void AddArticleService(this IServiceCollection services) 
    {
        services.AddScoped<IArticleService, ArticleService>();
    }
}