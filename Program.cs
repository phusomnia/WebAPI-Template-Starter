using System.Reflection;
using Scalar.AspNetCore;
using WebAPI_Template_Starter.Infrastructure.Annotation;
using WebAPI_Template_Starter.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAnnotation(Assembly.GetExecutingAssembly());
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
DatabaseConfig.configure(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
