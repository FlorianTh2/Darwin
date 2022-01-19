namespace hello_asp_identity.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Secret { get; set; } = null!;
    public TimeSpan AccessTokenLifetime { get; set; }

    public TimeSpan RefreshTokenLifetime { get; set; }
}