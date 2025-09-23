using System.Data;
using StackExchange.Redis;
using WebAPI_Template_Starter.SharedKernel.configuration;

namespace WebAPI_Template_Starter.Features.CacheAPI.Redis;

[Configuration]
public class NRedisConfig
{
    private readonly IConnectionMultiplexer _muxer;
    private readonly IDatabase _db;
    private readonly ConfigurationOptions _conf;

    public NRedisConfig()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        
        var url = config["Redis:url"];
        var port = int.TryParse(config["Redis:port"], out var p) ? p : 6379;
        var user = config["Redis:user"];
        var password = config["Redis:password"];
        var keepAlive = new DataTable().Compute(config["Redis:keepAlive"], null).ToString();
        
        _conf = new ConfigurationOptions
        {
            EndPoints = { { string.IsNullOrWhiteSpace(url) ? "localhost" : url, port } },
            Password = string.IsNullOrWhiteSpace(password) ? null : password,
            User = string.IsNullOrWhiteSpace(user) ? null : user
        };

        if (string.IsNullOrWhiteSpace(keepAlive))
        {
            _conf.KeepAlive = int.TryParse(keepAlive, out var ka) ? ka : 60;
        }

        _muxer = ConnectionMultiplexer.Connect(_conf);
        _db = _muxer.GetDatabase();
    }

    public IDatabase redis() => _db;
}