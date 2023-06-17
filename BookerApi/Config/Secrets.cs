using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BookerApi.Config;

public static class Secrets
{
    public static string DbConnectionString { get; set; } = string.Empty;
    public static string JwtIssuer { get; set; } = string.Empty;
    public static string JwtAccessSecret { get; set; } = string.Empty;
    public static string JwtRefreshSecret { get; set; } = string.Empty;
}