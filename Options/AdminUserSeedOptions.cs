namespace hello_asp_identity.Options;

public class AdminUserSeedOptions
{
    public const string SectionName = "AdminUserSeed";
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}