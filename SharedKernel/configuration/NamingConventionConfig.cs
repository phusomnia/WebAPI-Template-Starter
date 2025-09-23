namespace WebAPI_Template_Starter.SharedKernel.configuration;

public static class NamingConventionConfig
{
    public static IServiceCollection NamingConventionExtension(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.IncludeFields = true;
        });

        return services;
    }
}