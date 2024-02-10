using BergerDb.Api.Middlewares;
using BergerDb.Application;
using BergerDb.Domain;
using BergerDb.Infrastructure;
using BergerDb.Persistence;
using Carter;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDomain();

        services.AddPersistence(Configuration);

        services.AddInfrastructure(Configuration);

        services.AddApplication();

        services.AddSingleton(
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly()));

        services.AddScoped<IMapper, Mapper>();
        
        services.AddExceptionHandler<ExceptionHandler>();

        services.AddControllers();

        services.AddCarter();

        services.AddMapster();

        services.AddEndpointsApiExplorer();

        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", c =>
            {
                c.WithOrigins("http://localhost:5173")
                .WithOrigins("http://localhost:4173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
        });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddSwaggerGen(swaggerGenOptions =>
        {
            swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BergerDB API",
                Version = "v1"
            });

            swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseExceptionHandler("/error");

            app.UseSwagger();

            app.UseSwaggerUI(swaggerUiOptions => 
                swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "BergerDBs API"));
        }

        app.UseCors("CorsPolicy");

        app.UseAuthentication();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapCarter();
        });

        app.UseHttpsRedirection();
    }
}