using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using TestOnlineStore.Persistence;
using TestOnlineStore.WebAPI.Configurations;
using TestOnlineStore.WebAPI.Middlewares;

namespace TestOnlineStore.WebAPI;

public class Startup(IConfiguration configuration,
                     IWebHostEnvironment env)
{
    /// <summary>
    /// Конфигурация зависимостей
    /// </summary>
    /// <param name="services">Коллекция сервисов ServiceCollection</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddPersistence(configuration);
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("api-version"),
                new MediaTypeApiVersionReader("api-version"));
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }

    /// <summary>
    /// Конфигурация способов обработки запроса
    /// </summary>
    /// <param name="app">Приложение</param>
    /// <param name="provider">Провайдер версий приложения</param>
    public void Configure(IApplicationBuilder app,
                          IApiVersionDescriptionProvider provider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                config.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        app.UseCustomExceptionHandler();
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseEndpoints(configure: endpoints => endpoints.MapControllers());
    }
}