using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using WebAPI_Template_Starter.Infrastructure.Config;
using WebAPI_Template_Starter.Infrastructure.Database;
using WebAPI_Template_Starter.Infrastructure.Middleware;
using WebAPI_Template_Starter.Infrastructure.Security;
using WebAPI_Template_Starter.Infrastructure.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAnnotation(Assembly.GetExecutingAssembly());
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

ControllerConfig.Configure(builder);
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
