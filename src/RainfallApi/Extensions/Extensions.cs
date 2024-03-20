using FastEndpoints.Swagger;
using LanguageExt;
using NSwag;

namespace RainfallApi.Extensions;

public static class OpenApi
{
    public static DocumentOptions SetOptions(this DocumentOptions o, WebApplicationBuilder builder)
    {
        var info = builder.Configuration.GetSection("OpenApiInfo").Get<OpenApiInfo>();

        ArgumentNullException.ThrowIfNull(info, nameof(info));

        o.DocumentSettings = s =>
            {
                s.PostProcess = (d) =>
                {
                    d.Info = info;
                };
            };

        return o;
    }
}
