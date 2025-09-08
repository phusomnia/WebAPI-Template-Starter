using Microsoft.EntityFrameworkCore;
using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Infrastructure.Database;

namespace WebAPI_Template_Starter.Infrastructure.Config;

public static class DatabaseConfig
{
    public static IServiceCollection DatabaseConfigExtension(this IServiceCollection services, IConfiguration config)
    {
        var REMOTE_MYSQL_URL = config.GetConnectionString("REMOTE_MYSQL_URL");
        services.AddDbContext<BaseContext>(options => options.UseMySql(
            REMOTE_MYSQL_URL,
            ServerVersion.AutoDetect(REMOTE_MYSQL_URL)
        ));
        services.AddDbContext<AppDbContext>(options => options.UseMySql(
            REMOTE_MYSQL_URL,
            ServerVersion.AutoDetect(REMOTE_MYSQL_URL)
        ));

        return services;
    }
}