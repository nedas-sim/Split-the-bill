using Domain.Common;

namespace WebApi.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddConfig
        (this IServiceCollection @this, 
              IConfiguration config)
    {
        @this.AddOptions<ConnectionStrings>().Bind(config.GetSection(nameof(ConnectionStrings)));
        @this.AddOptions<UserSettings>().Bind(config.GetSection(nameof(UserSettings)));
        @this.AddOptions<JwtConfig>().Bind(config.GetSection(nameof(JwtConfig)));

        return @this;
    }
}
