using System.Text.Json.Serialization;
using Main.API.Extensions;
using Main.API.Persistance;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;

var builder = WebApplication.CreateBuilder(args);

// Add cors
var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins");

var origins = allowedOrigins.Split(',').ToArray();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Origins", policy =>
    {
        policy
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(origins)
            .Build();
    });
});

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
        .WithDefaultDestructurers());
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);

}, true);


// Add services to the container.

builder.Services.ConfigureEmailService(builder.Configuration);

builder.Services.AddControllers()
    .AddControllersAsServices()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.Converters.Add(new StringEnumConverter());
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDataServices();
builder.Services.AddValidators();

builder.Services.AddDbContext<MainDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("MainDbContext"),
        act => act.MigrationsAssembly("Main.API")));

builder.Services.ConfigureIdentity();

builder.Services.ConfigureSwagger();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = services.GetService<MainDbContext>();
        context?.Database.Migrate();
        logger.LogInformation("Seeding the DB.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Main API");
        x.RoutePrefix = string.Empty;
        x.DocumentTitle = "Main API";
    });
}

app.ConfigureExceptionHandler(app.Logger);

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("Origins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();