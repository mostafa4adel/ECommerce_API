using Serilog;
using Microsoft.EntityFrameworkCore;

using ECommerce_API.Data;
using ECommerce_API.Core.Configuration;
using ECommerce_API.Core.Repository;
using ECommerce_API.Core.Contracts;

using System.Text;
using ECommerce_API.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.OData;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using ECommerce_API.Core.Services;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DatabaseContext>();


var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "E-Commerce API",
            Version = "v1",
        }); 
    }
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});



builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<CategoriesServices>();
builder.Services.AddScoped<ProductsServices>();
builder.Services.AddScoped<ProductCategoryServices>();

builder.Services.AddScoped<ICategoriesRepositroy, CategoriesRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();




builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver")

        );
});

builder.Services.AddVersionedApiExplorer(

    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>("Custom Health Check",
    failureStatus: HealthStatus.Degraded
    , tags: new[] { "custom" })
    .AddMySql(
    connectionString: conn,
    name: "MySQL",
    failureStatus: HealthStatus.Degraded,
    tags: new[] { "db" })
    .AddDbContextCheck<DatabaseContext>();

builder.Services.AddControllers().AddOData(
    options =>
    {
        options.Select().Filter().OrderBy();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = healthcheck => healthcheck.Tags.Contains("custom"),
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },

    ResponseWriter = WriteResponse
});

app.MapHealthChecks("/health/db", new HealthCheckOptions
{
    Predicate = healthcheck => healthcheck.Tags.Contains("db"),
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
    ResponseWriter = WriteResponse
});


app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseCors("AllowAll");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static Task WriteResponse(HttpContext context, HealthReport healthReport)
{
    context.Response.ContentType = "application/json charset=utf-8";

    var options = new JsonWriterOptions
    {
        Indented = true
    };

    using var memoryStream = new MemoryStream();
    using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
    {
        jsonWriter.WriteStartObject();
        jsonWriter.WriteString("Status", healthReport.Status.ToString());
        jsonWriter.WriteStartObject("Results");

        foreach (var (key, value) in healthReport.Entries)
        {
            jsonWriter.WriteStartObject(key);
            jsonWriter.WriteString("Status", value.Status.ToString());
            jsonWriter.WriteString("Description", value.Description);
            jsonWriter.WriteStartObject("Data");
            foreach (var (dataKey, dataValue) in value.Data)
            {
                jsonWriter.WritePropertyName(dataKey);
                JsonSerializer.Serialize(jsonWriter, dataValue, dataValue?.GetType() ?? typeof(object));
            }
            jsonWriter.WriteEndObject();
            jsonWriter.WriteEndObject();

        }
        jsonWriter.WriteEndObject();
        jsonWriter.WriteEndObject();
    }

    return context.Response.WriteAsync(Encoding.UTF8.GetString(memoryStream.ToArray()));
}

class CustomHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = true;

        // custom checks. Logic etc.etc

        if (isHealthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("All Systems are looking good."));
        }

        return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "System Unhealthy"));
    }
}