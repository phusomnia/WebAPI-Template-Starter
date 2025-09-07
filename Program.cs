using System.Reflection;
using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using WebAPI_Template_Starter.Infrastructure.Annotation;
using WebAPI_Template_Starter.Infrastructure.Database;
using WebAPI_Template_Starter.Infrastructure.Middleware;
using WebAPI_Template_Starter.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAnnotation(Assembly.GetExecutingAssembly());
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
SecurityConfig.Configure(builder);
DatabaseConfig.configure(builder);

var app = builder.Build();

app.UseGlobalExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
