using System.Runtime.CompilerServices;

namespace RainfallApi.Extensions;

public record RainfallApiInfo(
    string Title,
    string Version,
    string Description,
    Contact Contact);

public record Contact(
    string Name,
    string Url);
