using Microsoft.OpenApi.Models;

namespace RainfallApi.Extensions;

public static class ConfigExtensions
{
    public static OpenApiInfo? GetOpenApiInfo(this ConfigurationManager config)
    {
        return config.GetSection("OpenApiInfo")?.Get<RainfallApiInfo>()?.ToOpenApiInfo();
    }

    public static OpenApiInfo ToOpenApiInfo(this RainfallApiInfo info)
    {
        ArgumentNullException.ThrowIfNull(info);

        return new OpenApiInfo
        {
            Title = info.Title,
            Version = info.Version,
            Description = info.Description,
            Contact = new OpenApiContact
            {
                Name = info.Contact?.Name,
                Url = new Uri(info.Contact?.Url ?? string.Empty)
            }
        };
    }
}