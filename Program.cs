using BusinessManagement.Configuration;
using BusinessManagement.Repositories;
using BusinessManagement.Supervisors;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Configuration
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings")
);

// Dependency Injection
builder.Services.AddSingleton<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IBusinessSupervisor, BusinessSupervisor>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Business Management API",
        Version = "v1",
        Description = "Business Management Module API"
    });
});

var app = builder.Build();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Business Management API V1");
    c.RoutePrefix = string.Empty;
});

// Disable for local development if HTTPS isn't configured
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();