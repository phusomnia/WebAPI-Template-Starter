using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using WebAPI_Template_Starter.Features.RealTimeAPI.Chat;
using WebAPI_Template_Starter.Infrastructure.Security;
using WebAPI_Template_Starter.SharedKernel.configuration;
using WebAPI_Template_Starter.SharedKernel.exception;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAnnotation(Assembly.GetExecutingAssembly());
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var config = builder.Configuration;
builder.Services.ControllerConfigExtension();
builder.Services.NamingConventionExtension(config);
builder.Services.SecurityConfigExtension(config);
builder.Services.DatabaseConfigExtension(config);
builder.Services.IntegrationConfigExtension(config);


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
app.UseCors("AllowAll");
app.MapHub<ChatHub>("/chatHub");

app.MapControllers();

app.Run();
