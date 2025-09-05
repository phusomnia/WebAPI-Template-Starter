using Microsoft.EntityFrameworkCore;
using WebAPI_Template_Starter.Domain.Entities;

namespace WebAPI_Template_Starter.Infrastructure.Database;

public class DatabaseConfig
{
    public static void configure(WebApplicationBuilder builder)
    {
        var REMOTE_MYSQL_URL = builder.Configuration.GetConnectionString("REMOTE_MYSQL_URL");
        builder.Services.AddDbContext<BaseContext>(options => options.UseMySql(
            REMOTE_MYSQL_URL,
            ServerVersion.AutoDetect(REMOTE_MYSQL_URL)
        ));
        builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(
            REMOTE_MYSQL_URL,
            ServerVersion.AutoDetect(REMOTE_MYSQL_URL)
        ));
    }
}